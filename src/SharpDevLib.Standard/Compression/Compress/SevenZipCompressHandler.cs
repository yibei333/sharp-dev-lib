﻿namespace SharpDevLib.Standard.Compression.Compress;

internal class SevenZipCompressHandler : CompressHandler
{
    public SevenZipCompressHandler(CompressOption option) : base(option)
    {
    }

    public override Task HandleAsync()
    {
        throw new NotSupportedException(".7z is not supported yet");
    }
}
