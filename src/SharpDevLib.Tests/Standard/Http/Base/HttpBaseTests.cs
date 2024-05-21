﻿using EmbedIO;
using EmbedIO.Files;
using EmbedIO.WebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;

namespace SharpDevLib.Tests.Standard.Http.Base;

public class HttpBaseTests
{
    private static WebServer? _server;
    protected const string BaseUrl = "http://localhost:23456";

    [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void Initialize(TestContext context)
    {
        _server = new WebServer(o => o.WithUrlPrefix(BaseUrl)
                        .WithMode(HttpListenerMode.EmbedIO))
                        .WithLocalSessionManager()
                        .WithWebApi("/api", m => m.WithController<ApiController>())
                        .WithStaticFolder("/statics", AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data"), true, m => m.WithContentCaching(true));
        _server.Start();
        HttpGlobalSettings.BaseUrl = BaseUrl;
    }

    [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void Cleanup()
    {
        _server?.Dispose();
    }
}
