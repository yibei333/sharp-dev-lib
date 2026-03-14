using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Basic.Helpers;

[TestClass]
public class ProcessHelperTests
{
    [TestMethod]
    public async Task StartAndWaitForExitAsyncWithEmptyFilenameTest()
    {
        using var process = new Process();
        var request = new ProcessStartRequest("", "");
        var result = await process.StartAndWaitForExitAsync(request);
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(-1, result.ExitCode);
        Assert.Contains("filename is empty", result.Error);
    }

    [TestMethod]
    public async Task StartAndWaitForExitAsyncWithNullFilenameTest()
    {
        using var process = new Process();
        var request = new ProcessStartRequest(null!, "");
        var result = await process.StartAndWaitForExitAsync(request);
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(-1, result.ExitCode);
        Assert.Contains("filename is empty", result.Error);
    }

    [TestMethod]
    [DataRow("cmd.exe", "/c echo Hello World")]
    [DataRow("powershell.exe", "-Command Write-Host 'Hello World'")]
    public async Task StartAndWaitForExitAsyncSuccessTest(string filename, string args)
    {
        using var process = new Process();
        var request = new ProcessStartRequest(filename, args);
        var result = await process.StartAndWaitForExitAsync(request);
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(0, result.ExitCode);
        Assert.IsTrue(result.Error.IsNullOrWhiteSpace());
        Assert.Contains("Hello World", result.Output);
    }

    [TestMethod]
    public async Task StartAndWaitForExitAsyncWithWorkingDirectoryTest()
    {
        using var process = new Process();
        var request = new ProcessStartRequest("cmd.exe", "/c cd")
        {
            WorkingDirectory = Environment.SystemDirectory,
        };
        var result = await process.StartAndWaitForExitAsync(request);
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(0, result.ExitCode);
        Assert.IsFalse(result.Output.IsNullOrWhiteSpace());
    }

    [TestMethod]
    public async Task StartAndWaitForExitAsyncWithEnvironmentVariablesTest()
    {
        using var process = new Process();
        Environment.SetEnvironmentVariable("TEST_VAR", "TestValue123");
        var request = new ProcessStartRequest("cmd.exe", "/c echo %TEST_VAR%");
        var result = await process.StartAndWaitForExitAsync(request);
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(0, result.ExitCode);
        Assert.Contains("TestValue123", result.Output);
    }

    [TestMethod]
    public async Task StartAndWaitForExitAsyncWithTimeoutTest()
    {
        using var process = new Process();
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));
        var request = new ProcessStartRequest("cmd.exe", "/c timeout /t 10")
        {
            CancellationToken = cancellationTokenSource.Token,
        };
        var result = await process.StartAndWaitForExitAsync(request);
        Console.WriteLine(result.IsSuccess);
        Assert.IsFalse(result.IsSuccess);
        Assert.IsFalse(result.Error.IsNullOrWhiteSpace());
    }

    [TestMethod]
    public async Task StartAndWaitForExitAsyncWithInvalidCommandTest()
    {
        using var process = new Process();
        var request = new ProcessStartRequest("nonexistentcommand12345.exe", "");
        var result = await process.StartAndWaitForExitAsync(request);
        Assert.IsFalse(result.IsSuccess);
        Assert.AreNotEqual(0, result.ExitCode);
    }
}