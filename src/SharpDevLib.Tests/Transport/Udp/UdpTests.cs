using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Transport.Udp;

[TestClass]
public class UdpTests
{
    [TestMethod]
    public void GetAvailbelUdpTest()
    {
        var port = UdpHelper.GetAvailableUdpPort(50, 100);
        Assert.IsGreaterThanOrEqualTo(50, port);
        Assert.IsLessThanOrEqualTo(100, port);
    }

    [TestMethod]
    public async Task SendFileTest()
    {
        //消息格式
        //第一个字节为消息类型,后面为内容
        //消息类型
        //1-文件信息
        //2-数据包,数据包格式,4个字节为分片索引,后续为内容
        //3-确认收到文件信息
        //4-确认收到包信息,4个字节为分片索引
        var builder = new StringBuilder();
        for (int i = 0; i < 10240; i++)
        {
            builder.AppendLine("Hello,World");
            builder.AppendLine("Hello,World");
            builder.AppendLine("Hello,World");
            builder.AppendLine("Hello,World");
            builder.AppendLine("Hello,World");
        }
        builder.ToString().Utf8Decode().SaveToFile("source.txt");
        var chunkSize = 1024 * 16;//16kb
        using var receiver = new Receiver(chunkSize);
        using var sender = new Sender(chunkSize, receiver.Port, "source.txt");
        sender.SendFile();
        await receiver.WaitForComplete();
        Console.WriteLine(receiver.Result);
    }

    class Sender : IDisposable
    {
        public Sender(int chunkSize, int receiverPort, string filePath)
        {
            var info = new FileInfo(filePath);
            FileInfo = new FileInfoDto(info.Name, info.Length, chunkSize);
            ReceiverPort = receiverPort;
            Stream = info.OpenRead();
            Port = UdpHelper.GetAvailableUdpPort(8000, 9000);
            Client = UdpHelper.CreateClient(IPAddress.Loopback, Port, chunkSize + 100);
            Client.Error += (_, e) => Console.WriteLine($"Sender异常:{e.Exception.Message}");
            Client.Received += HandleMessage;
            Client.StartReceive();
        }

        int ReceiverPort { get; }
        FileStream Stream { get; }
        int Port { get; }
        UdpClient Client { get; }
        FileInfoDto FileInfo { get; set; } = null!;
        bool IsFileInfoConfirmed { get; set; }
        readonly BlockingCollection<Packet> _packets = [];

        public void SendFile()
        {
            SendFileInfo();
            EnsureFileInfoSend();
        }

        void EnsureFileInfoSend()
        {
            var cancle = new CancellationTokenSource();
            cancle.CancelAfter(TimeSpan.FromMilliseconds(500));//0.5s后如果没有收到确认文件信息,重发
            cancle.Token.Register(() =>
            {
                if (IsFileInfoConfirmed) return;
                SendFile();
            });
        }

        void SendFileInfo()
        {
            byte[] bytes = [1, .. FileInfo.Serialize().Utf8Decode()];
            Client.Send(IPAddress.Loopback, ReceiverPort, bytes);
        }

        void SendSinglePacket(int index)
        {
            try
            {
                var buffer = new byte[FileInfo.ChunkSize];
                Stream.Seek(index * FileInfo.ChunkSize, SeekOrigin.Begin);
                var count = Stream.Read(buffer);
                byte[] bytes = [2, .. BitConverter.GetBytes(index), .. buffer.Take(count)];
                SetPacket(index);
                Client.Send(IPAddress.Loopback, ReceiverPort, bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发送异常{index}:{ex.Message}");
                SetPacket(index);
            }
        }

        void SetPacket(int index)
        {
            var packet = _packets.FirstOrDefault(x => x.Index == index);
            if (packet is null) _packets.Add(new Packet { Index = index, SendTime = DateTime.Now });
            else
            {
                packet.RetryCount++;
                packet.SendTime = DateTime.Now;
            }
        }

        void HandleMessage(object? sender, UdpClientDataEventArgs args)
        {
            var bytes = args.Bytes;
            var type = bytes[0];
            var data = bytes.Skip(1).ToArray();
            if (type == 3) HandleConfirmFileInfoMessage();
            else if (type == 4) HandleConfirmPacketMessage(data);
        }

        async void HandleConfirmFileInfoMessage()
        {
            IsFileInfoConfirmed = true;
            for (int i = 0; i < FileInfo.ChunkCount; i++)
            {
                SendSinglePacket(i);
                await Task.Delay(1);//防止太集中发送,丢包率太大
            }
        }

        void HandleConfirmPacketMessage(byte[] bytes)
        {
            var index = BitConverter.ToInt32(bytes);
            var packet = _packets.First(x => x.Index == index);
            packet.IsComplete = true;
            _packets.Where(x => !x.IsComplete && (DateTime.Now - x.SendTime > TimeSpan.FromSeconds(2))).ForEach(x =>
            {
                Console.WriteLine($"重发分片:{x.Index}");
                SendSinglePacket(x.Index);
            });
        }

        public void Dispose()
        {
            Stream.Dispose();
        }
    }

    class Receiver : IDisposable
    {
        public Receiver(int chunkSize)
        {
            Port = UdpHelper.GetAvailableUdpPort(8000, 9000);
            Client = UdpHelper.CreateClient(IPAddress.Loopback, Port, chunkSize + 100);
            Client.Error += (_, e) => Console.WriteLine($"Receiver异常:{e.Exception.Message}");
            Client.Received += HandleMessage;
            Client.StartReceive();
        }

        public int Port { get; }
        bool IsComplete => FileInfo is not null && FileInfo.ChunkCount == ReceiveChunkData.Count;
        public string Result => $"是否完成:{IsComplete}{(FileInfo is null ? "" : $",接收进度:{Math.Round(ReceiveChunkData.Count * 100m / FileInfo.ChunkCount, 2)}%")}";

        FileStream? Stream { get; set; }
        IPEndPoint? RemoteEndPoint { get; set; }
        UdpClient Client { get; }
        readonly List<int> ReceiveChunkData = [];
        FileInfoDto? FileInfo { get; set; }
        readonly CancellationTokenSource CompleteCancellationTokenSource = new();

        public async Task WaitForComplete()
        {
            try
            {
                await Task.Delay(TimeSpan.FromMinutes(20), CompleteCancellationTokenSource.Token);
            }
            catch
            {
            }
        }

        void SendConfirmFileInfo()
        {
            if (RemoteEndPoint is null) return;
            Client.Send(RemoteEndPoint.Address, RemoteEndPoint.Port, [3]);
        }

        void SendConfirmPacket(int index)
        {
            if (RemoteEndPoint is null) return;
            byte[] bytes = [4, .. BitConverter.GetBytes(index)];
            Client.Send(RemoteEndPoint.Address, RemoteEndPoint.Port, bytes);
        }

        void HandleMessage(object? sender, UdpClientDataEventArgs args)
        {
            var bytes = args.Bytes;
            var type = bytes[0];
            var data = bytes.Skip(1).ToArray();
            RemoteEndPoint = args.RemoteEndPoint;
            if (type == 1) HandleFileInfoMessage(data);
            else if (type == 2) HandlePacketMessage(data);
        }

        void HandleFileInfoMessage(byte[] bytes)
        {
            FileInfo = bytes.Utf8Encode().DeSerialize<FileInfoDto>();
            Stream = new FileStream($"Saved_{FileInfo.Name}", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            SendConfirmFileInfo();
        }

        void HandlePacketMessage(byte[] bytes)
        {
            if (FileInfo is null || Stream is null) return;
            var index = BitConverter.ToInt32(bytes.Take(4).ToArray());
            if (ReceiveChunkData.Contains(index))
            {
                SendConfirmPacket(index);
                Console.WriteLine($"分片{index}已处理,忽略");
                return;
            }
            var data = bytes.Skip(4).ToArray();
            Stream.Seek(index * FileInfo.ChunkSize, SeekOrigin.Begin);
            Stream.Write(data);
            ReceiveChunkData.Add(index);
            SendConfirmPacket(index);
            //Console.WriteLine($"接收进度:{Math.Round(ReceiveChunkData.Count * 100m / FileInfo.ChunkCount, 2)}%");
            if (IsComplete) CompleteCancellationTokenSource.Cancel();
        }

        public void Dispose()
        {
            Stream?.Dispose();
        }
    }

    class Packet
    {
        public int Index { get; set; }
        public DateTime SendTime { get; set; }
        public int RetryCount { get; set; }
        public bool IsComplete { get; set; }
    }

    class FileInfoDto(string name, long length, int chunkSize)
    {
        public string Name { get; } = name;
        public long Length { get; } = length;
        public int ChunkCount => (int)Math.Ceiling(Length * 1m / ChunkSize);
        public int ChunkSize { get; } = chunkSize;
    }
}