namespace SharpDevLib.Tests.Standard.Encode;

public abstract class EncodeTests
{
    protected const string _str = "foo";
    protected const string _hex = "666f6f";
    protected const string _base64 = "Zm9v";
    protected const string _url = "https://foo.com/bar?query=baz";
    protected const string _urlEncode = "https%3A%2F%2Ffoo.com%2Fbar%3Fquery%3Dbaz";
    protected const string _base64UrlEncode = "aHR0cHM6Ly9mb28uY29tL2Jhcj9xdWVyeT1iYXo";
    protected static readonly byte[] _bytes = [102, 111, 111];
    protected static readonly byte[] _emptyBytes = [];
}
