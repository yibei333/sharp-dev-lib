using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharpDevLib.Tests.Standard.Http;

public class HttpBaseTests
{
    [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void Initialize(TestContext context)
    {
        Console.WriteLine("init");
    }

    [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void Cleanup()
    {
        Console.WriteLine("clean");
    }
}
