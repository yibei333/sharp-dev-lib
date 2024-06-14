using System;
using System.Collections.Generic;
using System.Text;

namespace SharpDevLib.Cryptography;

internal class PemHeader
{
    public PemHeader(string title, string? procType, string? dEKInfo)
    {
        Title = title;
        ProcType = procType;
        DEKInfo = dEKInfo;
    }


    public string Title { get; }

    public string? ProcType { get; }

    public string? DEKInfo { get; }
}
