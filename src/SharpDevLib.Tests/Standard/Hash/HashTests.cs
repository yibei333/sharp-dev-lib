namespace SharpDevLib.Tests.Standard.Hash;

public abstract class HashTests
{
    protected readonly byte[] _emptyBytes = [];
    protected readonly byte[] _bytes = "foobar".ToUtf8Bytes();
    protected readonly byte[] _secret = "123456".ToUtf8Bytes();
}