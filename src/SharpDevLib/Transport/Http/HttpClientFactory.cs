namespace SharpDevLib;

internal class HttpClientFactory
{
    static readonly List<HttpClientInfo> _clients = [CreateClient(null, null)];

    static HttpClientInfo CreateClient(string? clientId, string? baseUrl)
    {
        var httpHandler = new HttpClientHandler { CookieContainer = new() };
        var client = new HttpClient(httpHandler);
        if (baseUrl.NotNullOrWhiteSpace()) client.BaseAddress = new Uri(baseUrl);
        return new HttpClientInfo(clientId, client, httpHandler);
    }

    public static void AddClient(string clientId, string? baseUrl)
    {
        if (clientId.IsNullOrWhiteSpace()) throw new Exception("HTTP客户端Id不能为空");
        if (_clients.Any(x => x.ClientId == clientId)) throw new Exception($"Id为'{clientId}'HTTP客户端已存在");
        _clients.Add(CreateClient(clientId, baseUrl));
    }

    public static HttpClientInfo GetClient(string? clientId)
    {
        if (clientId.IsNullOrWhiteSpace()) return _clients.First(x => x.ClientId.IsNullOrWhiteSpace());
        return _clients.FirstOrDefault(x => x.ClientId == clientId) ?? throw new Exception($"找不到Id为'{clientId}'的HTTP客户端");
    }
}

internal class HttpClientInfo(string? clientId, HttpClient client, HttpClientHandler clientHandler)
{
    public string? ClientId { get; } = clientId;
    public HttpClient Client { get; } = client;
    public HttpClientHandler ClientHandler { get; } = clientHandler;
}
