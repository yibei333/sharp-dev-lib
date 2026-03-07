using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;

namespace SharpDevLib.Tests.Basic.Helpers;

[TestClass]
public class ProcessHelperTests
{
    [TestMethod]
    public void StartAndWaitForExitAsyncWithEmptyFilenameTest()
    {
        using var process = new Process();
        var request = new ProcessStartRequest("", "");
        var result = process.StartAndWaitForExitAsync(request).GetAwaiter().GetResult();
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(-1, result.ExitCode);
        Assert.Contains("filename is empty", result.Error);
    }

    [TestMethod]
    public void StartAndWaitForExitAsyncWithNullFilenameTest()
    {
        using var process = new Process();
        var request = new ProcessStartRequest(null!, "");
        var result = process.StartAndWaitForExitAsync(request).GetAwaiter().GetResult();
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(-1, result.ExitCode);
        Assert.Contains("filename is empty", result.Error);
    }

    [TestMethod]
    [DataRow("cmd.exe", "/c echo Hello World")]
    [DataRow("powershell.exe", "-Command Write-Host 'Hello World'")]
    public void StartAndWaitForExitAsyncSuccessTest(string filename, string args)
    {
        using var process = new Process();
        var request = new ProcessStartRequest(filename, args);
        var result = process.StartAndWaitForExitAsync(request).GetAwaiter().GetResult();
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(0, result.ExitCode);
        Assert.IsTrue(result.Error.IsNullOrWhiteSpace());
        Assert.Contains("Hello World", result.Output);
    }

    [TestMethod]
    public void StartAndWaitForExitAsyncWithWorkingDirectoryTest()
    {
        using var process = new Process();
        var request = new ProcessStartRequest("cmd.exe", "/c cd")
        {
            WorkingDirectory = Environment.SystemDirectory,
        };
        var result = process.StartAndWaitForExitAsync(request).GetAwaiter().GetResult();
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(0, result.ExitCode);
        Assert.IsFalse(result.Output.IsNullOrWhiteSpace());
    }

    [TestMethod]
    public void StartAndWaitForExitAsyncWithEnvironmentVariablesTest()
    {
        using var process = new Process();
        Environment.SetEnvironmentVariable("TEST_VAR", "TestValue123");
        var request = new ProcessStartRequest("cmd.exe", "/c echo %TEST_VAR%");
        var result = process.StartAndWaitForExitAsync(request).GetAwaiter().GetResult();
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(0, result.ExitCode);
        Assert.Contains("TestValue123", result.Output);
    }

    [TestMethod]
    public void StartAndWaitForExitAsyncWithTimeoutTest()
    {
        using var process = new Process();
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));
        var request = new ProcessStartRequest("cmd.exe", "/c timeout /t 10")
        {
            CancellationToken = cancellationTokenSource.Token,
        };
        var result = process.StartAndWaitForExitAsync(request).GetAwaiter().GetResult();
        Assert.IsFalse(result.IsSuccess);
        Assert.IsFalse(result.Error.IsNullOrWhiteSpace());
    }

    [TestMethod]
    public void StartAndWaitForExitAsyncWithInvalidCommandTest()
    {
        using var process = new Process();
        var request = new ProcessStartRequest("nonexistentcommand12345.exe", "");
        var result = process.StartAndWaitForExitAsync(request).GetAwaiter().GetResult();
        Assert.IsFalse(result.IsSuccess);
        Assert.AreNotEqual(0, result.ExitCode);
    }
}