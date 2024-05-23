using System;
using System.IO;
using System.Text;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Pop3.Lib;

public static class StreamLineReader
{
    public delegate void OnReadLineDelegate(Line line);
    public delegate void OnCloseStreamDelegate();

    [System.Diagnostics.DebuggerDisplay("{AsASCII}")]
    public class Line
    {
        public long Sequence { get; }
        public byte[] Bytes { get; }
        public bool IsCompleteLine { get; }
        public string AsASCII => Encoding.ASCII.GetString(Bytes);
        public string AsUTF8 => Encoding.UTF8.GetString(Bytes);
        public Action StopReader { get; }

        internal Line(byte[] bytes, long sequence, bool isCompleteLine, Action stopReader)
        {
            Bytes = bytes;
            Sequence = sequence;
            IsCompleteLine = isCompleteLine;
            StopReader = stopReader;
        }
    }

    public static void Start(Stream stream, int bufferSize, OnReadLineDelegate onReadLine, OnCloseStreamDelegate onCloseStream)
    {
        var mutex = new object();
        long nextLineSequence = 0;

        var buffer = new byte[bufferSize];
        int startIndex = 0;
        int usedLength = 0;
        int availIndex() => startIndex + usedLength;
        int availLength() => buffer.Length - availIndex();
        bool expectLF = true;
        const byte CR = 13;
        const byte LF = 10;

        bool stopReaderNow = false;
        void StopReaderByCaller() => stopReaderNow = true;

        InvokeBeginRead();

        void InvokeBeginRead()
        {
            Helpers.TryCallCatch(BeginReadInternal);
            void BeginReadInternal() => stream.BeginRead(buffer, availIndex(), availLength(), ReadCallBack, null);
        }

        void ReadCallBack(IAsyncResult iar)
        {
            lock (mutex)
            {
                int newByteCount = 0;
                Helpers.TryCallCatch(EndReadInternal);
                void EndReadInternal() => newByteCount = stream.EndRead(iar);

                if (newByteCount == 0)
                {
                    if (usedLength > 0)
                    {
                        var lastLine = ExtractBytes(buffer, startIndex, usedLength);
                        CallOnReadLine(lastLine, true);
                    }

                    onCloseStream();
                    return;
                }

                usedLength += newByteCount;

                if (expectLF && usedLength > 0)
                {
                    if (buffer[startIndex] == LF)
                    {
                        startIndex += 1;
                        usedLength -= 1;
                    }
                    expectLF = false;
                }

                int scanIndex = startIndex;
                while (scanIndex < availIndex())
                {
                    byte atOffset = buffer[scanIndex];
                    if (atOffset == CR || atOffset == LF)
                    {
                        var lineByteCount = scanIndex - startIndex;
                        var line = ExtractBytes(buffer, startIndex, lineByteCount);
                        var lineByteCountIncludingEndOfLine = lineByteCount + 1;
                        startIndex += lineByteCountIncludingEndOfLine;
                        usedLength -= lineByteCountIncludingEndOfLine;

                        if (atOffset == CR)
                        {
                            if (usedLength > 0 && buffer[startIndex] == LF)
                            {
                                startIndex += 1;
                                usedLength -= 1;
                                scanIndex += 1;
                            }
                            else if (usedLength == 0) expectLF = true;
                        }

                        CallOnReadLine(line, true);
                    }

                    scanIndex++;
                }

                if (usedLength == 0) startIndex = 0;

                if (availLength() == 0 && startIndex > 0)
                {
                    if (usedLength > 0) Buffer.BlockCopy(buffer, startIndex, buffer, 0, usedLength);
                    startIndex = 0;
                }

                if (startIndex == 0 && availLength() == 0)
                {
                    CallOnReadLine([.. buffer], false);
                    startIndex = 0;
                    usedLength = 0;
                }

                if (stopReaderNow == false) InvokeBeginRead();
                void CallOnReadLine(byte[] line, bool isCompleteLine) => onReadLine(new Line(line, nextLineSequence++, isCompleteLine, StopReaderByCaller));
            }
        }
    }

    private static byte[] ExtractBytes(byte[] from, int startIndex, int length)
    {
        var toBuffer = new byte[length];
        Buffer.BlockCopy(from, startIndex, toBuffer, 0, length);
        return toBuffer;
    }
}
