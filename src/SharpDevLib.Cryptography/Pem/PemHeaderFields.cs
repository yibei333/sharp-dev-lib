using SharpDevLib.Cryptography.Internal.References;

namespace SharpDevLib.Cryptography.Pem;

internal class PemHeaderFields
{
    public PemHeaderFields(string? procType, string? dEKInfo)
    {
        ProcType = procType;
        DEKInfo = dEKInfo;

        if (DEKInfo.NotNullOrWhiteSpace())
        {
            var array = DEKInfo?.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Last().Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (array is null || array.Length != 2) throw new Exception("DEK-INFO require 2 fields");
            DEKInfoAlgorithm = array[0];
            DEKInfoAlgorithmFileds = array[0].Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            DEKInfoIV = array[1];
            DEKInfoIVBytes = DEKInfoIV.HexStringDecode();
        }
    }

    public string? ProcType { get; }

    public string? DEKInfo { get; }

    public string? DEKInfoAlgorithm { get; }

    public string[]? DEKInfoAlgorithmFileds { get; }

    public string? DEKInfoIV { get; }

    public byte[]? DEKInfoIVBytes { get; }
}
