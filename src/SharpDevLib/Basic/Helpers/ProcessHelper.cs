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
    /// 启动进程
    /// </summary>
    /// <param name="process">要启动的进程示例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <param name="onStandardOutput">标准输出</param>
    /// <param name="onErrotOutput">错误输出</param>
    /// <returns>进程执行结果</returns>
    public static async void Start
    (
        this Process process,
        CancellationToken? cancellationToken = null,
        Action<string>? onStandardOutput = null,
        Action<string>? onErrotOutput = null
    )
    {
        await process.StartAndWaitForExitAsync(cancellationToken, onStandardOutput, onErrotOutput);
    }

    /// <summary>
    /// 启动进程
    /// </summary>
    /// <param name="fileName">可执行文件名</param>
    /// <param name="args">命令行参数</param>
    /// <param name="workingDirectory">工作目录</param>
    /// <param name="cancellationToken">取消令牌</param>    
    /// <param name="encoding">输出编码</param>
    /// <param name="onStandardOutput">标准输出</param>
    /// <param name="onErrotOutput">错误输出</param>
    /// <returns>进程执行结果</returns>
    public static async void Start
    (
        string fileName,
        string? args = null,
        string? workingDirectory = null,
        Encoding? encoding = null,
        CancellationToken? cancellationToken = null,
        Action<string>? onStandardOutput = null,
        Action<string>? onErrotOutput = null
    )
    {
        await StartAndWaitForExitAsync(fileName, args, workingDirectory, encoding, cancellationToken, onStandardOutput, onErrotOutput);
    }

    /// <summary>
    /// 启动进程并等待其退出
    /// </summary>
    /// <param name="process">要启动的进程示例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <param name="onStandardOutput">标准输出</param>
    /// <param name="onErrotOutput">错误输出</param>
    /// <returns>进程执行结果</returns>
    public static async Task<ProcessResult> StartAndWaitForExitAsync
    (
        this Process process,
        CancellationToken? cancellationToken = null,
        Action<string>? onStandardOutput = null,
        Action<string>? onErrotOutput = null
    )
    {
        if (process.StartInfo is null || process.StartInfo.FileName.IsNullOrWhiteSpace()) throw new ArgumentException("StartInfo not set yet");
        var request = new ProcessStartRequest(process.StartInfo.FileName, process.StartInfo.Arguments)
        {
            WorkingDirectory = process.StartInfo.WorkingDirectory,
            CancellationToken = cancellationToken,
            OnStandardOutput = onStandardOutput,
            OnErrotOutput = onErrotOutput
        };
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
                onStandardOutput?.Invoke(args.Data);
            }
        }
        void OnErrorData(object sender, DataReceivedEventArgs args)
        {
            if (args.Data.NotNullOrWhiteSpace())
            {
                lock (lockObj) errorBuilder.AppendLine(args.Data);
                onErrotOutput?.Invoke(args.Data);
            }
        }

        try
        {
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += OnOutputData;
            process.ErrorDataReceived += OnErrorData;

            EventHandler exitedHandler = null!;
            exitedHandler = (s, e) =>
            {
                result.ExitCode = process.ExitCode;
                result.Output = outputBuilder.ToString();
                result.Error = errorBuilder.ToString();
                tcs.TrySetResult(result);
            };
            process.EnableRaisingEvents = true;
            process.Exited += exitedHandler;

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

            if (request.CancellationToken.HasValue && request.CancellationToken.Value.IsCancellationRequested)
            {
                tcs.TrySetCanceled(request.CancellationToken.Value);
                return await tcs.Task.ConfigureAwait(false);
            }
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return await tcs.Task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            result.ExitCode = -1;
            result.Error = ex.Message;
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
    /// <param name="fileName">可执行文件名</param>
    /// <param name="args">命令行参数</param>
    /// <param name="workingDirectory">工作目录</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <param name="encoding">输出编码</param>
    /// <param name="onStandardOutput">标准输出</param>
    /// <param name="onErrotOutput">错误输出</param>
    /// <returns>进程执行结果</returns>
    public static async Task<ProcessResult> StartAndWaitForExitAsync
    (
        string fileName,
        string? args = null,
        string? workingDirectory = null,
        Encoding? encoding = null,
        CancellationToken? cancellationToken = null,
        Action<string>? onStandardOutput = null,
        Action<string>? onErrotOutput = null
    )
    {
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo(fileName, args ?? "")
            {
                WorkingDirectory = workingDirectory,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            }
        };
        if (encoding is not null)
        {
            process.StartInfo.StandardOutputEncoding = encoding;
            process.StartInfo.StandardErrorEncoding = encoding;
        }
        return await process.StartAndWaitForExitAsync(cancellationToken, onStandardOutput, onErrotOutput);
    }
}

/// <summary>
/// 进程启动请求
/// </summary>
/// <param name="fileName">可执行文件名</param>
/// <param name="args">命令行参数</param>
internal class ProcessStartRequest(string fileName, string? args = null)
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
    /// 标准输出
    /// </summary>
    public Action<string>? OnStandardOutput { get; set; }

    /// <summary>
    /// 错误输出
    /// </summary>
    public Action<string>? OnErrotOutput { get; set; }
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
            ProcessHelper.Logger?.LogError("run process failed:{Path} {Args}", Request.Filename, Request.Args);
            if (Output.NotNullOrWhiteSpace()) ProcessHelper.Logger?.LogInformation("output:\r\n{Output}\r\n", Output);
            if (Error.NotNullOrWhiteSpace()) ProcessHelper.Logger?.LogError("error:\r\n{Error}\r\n", Error);
            throw new Exception(Error);
        }
    }

    internal ProcessStartRequest Request { get; set; } = null!;
}

/// <summary>
/// 进程扩展
/// </summary>
public static class ProcessExtensions
{
    /// <summary>
    /// 确保成功
    /// </summary>
    /// <exception cref="Exception">失败时抛出异常</exception>
    public static async Task<ProcessResult> EnsureSucceed(this Task<ProcessResult> processResult)
    {
        var result = await processResult;
        result.EnsureSucceed();
        return result;
    }
}