﻿namespace SharpDevLib.Standard.Compression.DeCompress;

internal class Bz2DeCompressHandler : DeCompressHandler
{
    public Bz2DeCompressHandler(DeCompressOption option) : base(option)
    {
    }

    public override Task HandleAsync()
    {
        throw new NotImplementedException();
    }
}
