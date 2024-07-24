namespace SharpDevLib.Tests.Standard.Hash;

public abstract class HashTests
{
    protected readonly byte[] _emptyBytes = [];
    protected readonly byte[] _bytes = "foobar".Utf8Decode();
    protected readonly byte[] _secret = "123456".Utf8Decode();
}