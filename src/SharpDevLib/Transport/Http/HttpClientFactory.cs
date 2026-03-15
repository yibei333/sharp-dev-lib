namespace SharpDevLib;

internal class HttpClientFactory
{
    static HttpClientFactory()
    {
        SetConfig(_internalDefaultClientId, new HttpConfig());
    }
    static readonly List<HttpClientInfo> _clients = [];
    static readonly object _locker = new();
    static readonly string _defaultClientId = Guid.NewGuid().ToString();
    static readonly string _internalDefaultClientId = Guid.NewGuid().ToString();

    public static HttpClientInfo GetClient(string? clientId)
    {
        if (clientId.IsNullOrWhiteSpace()) return _clients.FirstOrDefault(x => x.ClientId == _defaultClientId) ?? _clients.FirstOrDefault(x => x.ClientId == _internalDefaultClientId);
        return _clients.FirstOrDefault(x => x.ClientId == clientId) ?? throw new Exception($"找不到Id为'{clientId}'的客户端,请先调用HttpHelper.SetConfig设置");
    }

    public static void SetConfig(string clientId, HttpConfig config)
    {
        if (clientId.IsNullOrWhiteSpace()) throw new Exception("HTTP客户端Id不能为空");
        if (config is null) throw new Exception("配置不能为空");

        lock (_locker)
        {
            if (_clients.Any(x => x.ClientId == clientId)) throw new Exception($"HTTP客户端Id:{clientId}已经设置过了,只能设置一次");
            var httpHandler = new HttpClientHandler { CookieContainer = new() };
            var client = new HttpClient(httpHandler);
            if (config.BaseUrl.NotNullOrWhiteSpace()) client.BaseAddress = new Uri(config.BaseUrl);
            if (config.Timeout is not null) client.Timeout = config.Timeout.Value;
            var clientInfo = new HttpClientInfo(clientId, config, client, httpHandler);
            _clients.Add(clientInfo);
        }
    }

    public static void SetDefaultConfig(HttpConfig config) => SetConfig(_defaultClientId, config);
}

internal class HttpClientInfo(string clientId, HttpConfig config, HttpClient client, HttpClientHandler clientHandler)
{
    public string ClientId { get; } = clientId;
    public HttpConfig Config { get; } = config;
    public HttpClient Client { get; } = client;
    public HttpClientHandler ClientHandler { get; } = clientHandler;
}
