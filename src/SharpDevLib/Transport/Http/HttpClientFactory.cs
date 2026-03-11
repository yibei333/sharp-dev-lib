namespace SharpDevLib;

internal class HttpClientFactory
{
    static readonly List<HttpClientInfo> _clients = [];

    public static HttpClientInfo GetClient(HttpRequest request)
    {
        var clientId = request.ClientId.IsNullOrWhiteSpace() ? string.Empty : request.ClientId;
        var baseUrl = request.Config?.BaseUrl ?? HttpConfig.Default.BaseUrl;
        if (baseUrl.IsNullOrWhiteSpace()) baseUrl = string.Empty;
        var timeout = request.Config?.Timeout ?? HttpConfig.Default.Timeout;
        var clientInfo = _clients.FirstOrDefault(x => x.ClientId == clientId && x.BaseUrl == baseUrl && x.Timeout == timeout);
        if (clientInfo is null)
        {
            var httpHandler = new HttpClientHandler { CookieContainer = new() };
            var client = new HttpClient(httpHandler);
            if (baseUrl.NotNullOrWhiteSpace()) client.BaseAddress = new Uri(baseUrl);
            if (timeout is not null) client.Timeout = timeout.Value;
            clientInfo = new HttpClientInfo(clientId, baseUrl, timeout, client, httpHandler);
            _clients.Add(clientInfo);
            if (_clients.Count > 100) throw new Exception("HttpClient缓存数量已达上限100个,请重新规划HttpClient参数");
        }
        request.Cookies?.ForEach(clientInfo.ClientHandler.CookieContainer.Add);
        return clientInfo;
    }
}

internal class HttpClientInfo(string clientId, string? baseUrl, TimeSpan? timeout, HttpClient client, HttpClientHandler clientHandler)
{
    public string ClientId { get; } = clientId;
    public string? BaseUrl { get; } = baseUrl;
    public TimeSpan? Timeout { get; } = timeout;
    public HttpClient Client { get; } = client;
    public HttpClientHandler ClientHandler { get; } = clientHandler;
}
