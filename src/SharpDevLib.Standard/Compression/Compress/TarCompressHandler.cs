using ICSharpCode.SharpZipLib.Tar;
using System.Text;

namespace SharpDevLib.Standard.Compression.Compress;

internal class TarCompressHandler : CompressHandler<TarOutputStream, TarEntry>
{
    public TarCompressHandler(CompressOption option) : base(option)
    {
    }

    public override TarEntry CreateEntry(string key, string path)
    {
        var entry = TarEntry.CreateEntryFromFile(path);
        entry.Name = key;
        Console.WriteLine($"{key}->{entry.IsDirectory}");
        return entry;
    }

    public override TarOutputStream CreateStream(Stream targetStream)
    {
        return new TarOutputStream(targetStream, Encoding.ASCII) { Position = 0, IsStreamOwner = false };
    }

    public override void PutNextEntry(TarOutputStream outputStream, TarEntry entry)
    {
        outputStream.PutNextEntry(entry);
    }

    protected override async Task CopyEntryStreamToOutputStream(Stream entryStream, TarOutputStream outputStream)
    {
        var buffer = new byte[2];
        int length;
        while ((length = entryStream.Read(buffer, 0, buffer.Length)) > 0)
        {
            outputStream.Write(buffer, 0, length);
        }
        await Task.CompletedTask;
    }
}