using EmbedIO;
using EmbedIO.Files;
using EmbedIO.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Transport;
using System;
using System.Threading;

namespace SharpDevLib.Tests.Transport.Http.Base;

public class HttpBaseTests
{
    static WebServer? _server;
    protected const string BaseUrl = "http://localhost:23456";

    [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void Initialize(TestContext context)
    {
        Console.WriteLine(context.TestRunDirectory);
        _server = new WebServer(o => o.WithUrlPrefix(BaseUrl)
                        .WithMode(HttpListenerMode.EmbedIO))
                        .WithLocalSessionManager()
                        .WithWebApi("/api", m => m
                            .WithController<HttpGetController>()
                            .WithController<HttpPostController>()
                            .WithController<HttpPutController>()
                            .WithController<HttpDeleteController>()
                        )
                        .WithStaticFolder("/statics", AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData"), true, m => m.WithContentCaching(true));
        _server.Start(CancellationToken.None);
        HttpGlobalOptions.BaseUrl = BaseUrl;
    }

    [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void Cleanup()
    {
        _server?.Dispose();
    }
}
