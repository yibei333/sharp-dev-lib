﻿using ICSharpCode.SharpZipLib.Zip;

namespace SharpDevLib.Standard.Compression.Compress;

internal class ZipCompressHandler : CompressHandler<ZipOutputStream, ZipEntry>
{
    public ZipCompressHandler(CompressOption option) : base(option)
    {
    }

    public override bool SupportPassword => true;

    public override ZipEntry CreateEntry(string key, string path) => new(path);

    public override ZipOutputStream CreateStream(Stream sourceStream)
    {
        var outputStream = new ZipOutputStream(sourceStream)
        {
            Password = Option.Password,
            UseZip64 = UseZip64.Dynamic,
        };
        outputStream.SetLevel(Option.Level.ConvertToSharpZipLibLevel());
        return outputStream;
    }

    public override void PutNextEntry(ZipOutputStream stream, ZipEntry entry)
    {
        stream.PutNextEntry(entry);
    }
}
