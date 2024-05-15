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
        return entry;
    }

    public override TarOutputStream CreateStream(Stream sourceStream)
    {
        return new TarOutputStream(sourceStream, Encoding.UTF8);
    }

    public override void PutNextEntry(TarOutputStream stream, TarEntry entry)
    {
        stream.PutNextEntry(entry);
    }
}