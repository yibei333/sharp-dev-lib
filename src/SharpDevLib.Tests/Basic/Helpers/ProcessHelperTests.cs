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
        await Assert.ThrowsExactlyAsync<ArgumentException>(() => process.StartAndWaitForExitAsync());
    }

    [TestMethod]
    [DataRow("cmd.exe", "/c echo Hello World")]
    [DataRow("powershell.exe", "-Command Write-Host 'Hello World'")]
    public async Task StartAndWaitForExitAsyncSuccessTest(string filename, string args)
    {
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = filename,
                Arguments = args
            }
        };
        var result = await process.StartAndWaitForExitAsync().EnsureSucceed();
        Assert.AreEqual(0, result.ExitCode);
        Assert.IsTrue(result.Error.IsNullOrWhiteSpace());
        Assert.Contains("Hello World", result.Output);
    }

    [TestMethod]
    public async Task StartAndWaitForExitAsyncWithWorkingDirectoryTest()
    {
        using var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c cd",
                WorkingDirectory = Environment.SystemDirectory
            }
        };
        var result = await process.StartAndWaitForExitAsync().EnsureSucceed();
        Assert.AreEqual(0, result.ExitCode);
        Assert.IsFalse(result.Output.IsNullOrWhiteSpace());
    }

    [TestMethod]
    public async Task StartAndWaitForExitAsyncWithEnvironmentVariablesTest()
    {
        using var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c echo %TEST_VAR%",
            }
        };
        Environment.SetEnvironmentVariable("TEST_VAR", "TestValue123");
        var result = await process.StartAndWaitForExitAsync().EnsureSucceed();
        Assert.AreEqual(0, result.ExitCode);
        Assert.Contains("TestValue123", result.Output);
    }

    [TestMethod]
    public async Task StartAndWaitForExitAsyncWithTimeoutTest()
    {
        using var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c timeout /t 10",
            }
        };
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));
        var result = await process.StartAndWaitForExitAsync(cancellationToken: cancellationTokenSource.Token);
        Assert.IsFalse(result.Error.IsNullOrWhiteSpace());
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task StartAndWaitForExitAsyncWithInvalidCommandTest()
    {
        var result = await ProcessHelper.StartAndWaitForExitAsync("nonexistentcommand12345.exe", "");
        Assert.AreNotEqual(0, result.ExitCode);
        Assert.IsFalse(result.IsSuccess);
    }
}