using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// 进程帮助类，提供进程启动、执行和管理功能
/// </summary>
public static class ProcessHelper
{
    /// <summary>
    /// 获取或设置日志记录器，用于记录进程执行信息
    /// </summary>
    public static ILogger? Logger { get; set; } = new SimpleConsoleLogger(nameof(ProcessHelper));

    /// <summary>
    /// 启动进程并等待其退出
    /// </summary>
    /// <param name="process">要启动的进程示例</param>
    /// <param name="request">进程启动请求配置</param>
    /// <returns>进程执行结果</returns>
    public static async Task<ProcessResult> StartAndWaitForExitAsync(this Process process, ProcessStartRequest request)
    {
        if (request.Filename.IsNullOrWhiteSpace()) return new ProcessResult { Request = request, Error = $"filename is empty", ExitCode = -1 };
        var tcs = new TaskCompletionSource<ProcessResult>();
        var result = new ProcessResult { Request = request };
        var outputBuilder = new StringBuilder();
        var errorBuilder = new StringBuilder();
        var lockObj = new object();
        CancellationTokenRegistration? ctr = null;
        void OnOutputData(object sender, DataReceivedEventArgs args)
        {
            if (args.Data.NotNullOrWhiteSpace())
            {
                lock (lockObj) outputBuilder.AppendLine(args.Data);
                if (request.LogInfo) Logger?.LogInformation("{Output}", args.Data);
            }
        }
        void OnErrorData(object sender, DataReceivedEventArgs args)
        {
            if (args.Data.NotNullOrWhiteSpace())
            {
                lock (lockObj) errorBuilder.AppendLine(args.Data);
                if (request.LogInfo) Logger?.LogInformation("{Output}", args.Data);
            }
        }

        try
        {
            process.StartInfo = new ProcessStartInfo(request.Filename, request.Args ?? "")
            {
                WorkingDirectory = request.WorkingDirectory,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardErrorEncoding = Encoding.UTF8,
                StandardOutputEncoding = Encoding.UTF8
            };
            process.OutputDataReceived += OnOutputData;
            process.ErrorDataReceived += OnErrorData;

            EventHandler exitedHandler = null!;
            exitedHandler = (s, e) =>
            {
                process.Exited -= exitedHandler;
                result.ExitCode = process.ExitCode;
                result.Output = outputBuilder.ToString();
                result.Error = errorBuilder.ToString();
                tcs.TrySetResult(result);
            };
            process.EnableRaisingEvents = true;

            if (request.CancellationToken.HasValue)
            {
                ctr = request.CancellationToken.Value.Register(() =>
                {
                    try
                    {
                        if (!process.HasExited)
                        {
                            process.Kill();
                            tcs.TrySetCanceled(request.CancellationToken.Value);
                        }
                    }
                    catch (InvalidOperationException) when (process.HasExited)
                    {
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                    finally
                    {
                        process.Exited -= exitedHandler;
                    }
                });
            }
            else process.Exited += exitedHandler;

            if (request.CancellationToken.HasValue && request.CancellationToken.Value.IsCancellationRequested)
            {
                tcs.TrySetCanceled(request.CancellationToken.Value);
                return await tcs.Task.ConfigureAwait(false);
            }
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            if (process.HasExited)
            {
                process.Exited -= exitedHandler;
                result.ExitCode = process.ExitCode;
                result.Output = outputBuilder.ToString();
                result.Error = errorBuilder.ToString();
                tcs.TrySetResult(result);
            }
            return await tcs.Task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            result.ExitCode = -1;
            result.Error = ex.Message;
            if (request.LogError) Logger?.LogError(ex, "启动并运行进程失败:{Path} {Args}", request.Filename, request.Args);
            return result;
        }
        finally
        {
            ctr?.Dispose();
            process.OutputDataReceived -= OnOutputData;
            process.ErrorDataReceived -= OnErrorData;
        }
    }

    /// <summary>
    /// 启动进程并等待退出
    /// </summary>
    /// <param name="request">进程启动请求</param>
    /// <returns>进程执行结果</returns>
    public static async Task<ProcessResult> StartAndWaitForExitAsync(this ProcessStartRequest request)
    {
        using var process = new Process();
        return await process.StartAndWaitForExitAsync(request);
    }
}

/// <summary>
/// 进程启动请求
/// </summary>
/// <param name="fileName">可执行文件名</param>
/// <param name="args">命令行参数</param>
public class ProcessStartRequest(string fileName, string? args = null)
{
    /// <summary>
    /// 可执行文件名
    /// </summary>
    public string Filename { get; set; } = fileName;

    /// <summary>
    /// 命令行参数
    /// </summary>
    public string? Args { get; set; } = args;

    /// <summary>
    /// 工作目录
    /// </summary>
    public string? WorkingDirectory { get; set; }

    /// <summary>
    /// 取消令牌
    /// </summary>
    public CancellationToken? CancellationToken { get; set; }

    /// <summary>
    /// 是否记录信息日志
    /// </summary>
    public bool LogInfo { get; set; }

    /// <summary>
    /// 是否记录错误日志
    /// </summary>
    public bool LogError { get; set; } = true;
}

/// <summary>
/// 进程执行结果
/// </summary>
public class ProcessResult
{
    /// <summary>
    /// 退出码
    /// </summary>
    public int ExitCode { get; set; }

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool IsSuccess => ExitCode == 0;

    /// <summary>
    /// 标准输出
    /// </summary>
    public string Output { get; set; } = string.Empty;

    /// <summary>
    /// 错误输出
    /// </summary>
    public string Error { get; set; } = string.Empty;

    /// <summary>
    /// 确保成功
    /// </summary>
    /// <exception cref="Exception">失败时抛出异常</exception>
    public void EnsureSucceed()
    {
        if (!IsSuccess)
        {
            if (Request.LogError) ProcessHelper.Logger?.LogError("run process failed:{Path} {Args}", Request.Filename, Request.Args);
            if (Request.LogInfo && Output.NotNullOrWhiteSpace()) ProcessHelper.Logger?.LogInformation("output:\r\n{Output}\r\n", Output);
            if (Request.LogError && Error.NotNullOrWhiteSpace()) ProcessHelper.Logger?.LogError("error:\r\n{Error}\r\n", Error);
            throw new Exception(Error);
        }
    }

    internal ProcessStartRequest Request { get; set; } = null!;
}