using Microsoft.Extensions.Logging;

namespace SharpDevLib;

/// <summary>
/// 简单的控制台日志记录器，提供将日志输出到控制台的功能
/// </summary>
public class SimpleConsoleLogger(string categoryName, LogLevel minLevel = LogLevel.Information) : ILogger
{
    readonly string _categoryName = categoryName;
    readonly LogLevel _minLevel = minLevel;

    /// <summary>
    /// 开始日志作用域，返回一个空的作用域实例
    /// </summary>
    /// <typeparam name="TState">作用域状态类型</typeparam>
    /// <param name="state">作用域状态数据</param>
    /// <returns>作用域对象</returns>
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => NullScope.Instance;

    /// <summary>
    /// 检查指定日志级别是否已启用
    /// </summary>
    /// <param name="logLevel">日志级别</param>
    /// <returns>如果日志级别已启用返回true，否则返回false</returns>
    public bool IsEnabled(LogLevel logLevel) => logLevel >= _minLevel && logLevel != LogLevel.None;

    /// <summary>
    /// 记录日志消息到控制台
    /// </summary>
    /// <typeparam name="TState">日志状态类型</typeparam>
    /// <param name="logLevel">日志级别</param>
    /// <param name="eventId">事件ID</param>
    /// <param name="state">日志状态数据</param>
    /// <param name="exception">异常对象</param>
    /// <param name="formatter">格式化函数，用于生成日志消息</param>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;
        string message = formatter(state, exception);
        string log = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{logLevel}] [{_categoryName}] {message}";
        if (exception is not null) log += Environment.NewLine + exception.ToString();
        ConsoleColor originalColor = Console.ForegroundColor;
        switch (logLevel)
        {
            case LogLevel.Trace:
            case LogLevel.Debug:
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
            case LogLevel.Information:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case LogLevel.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case LogLevel.Error:
            case LogLevel.Critical:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
        }
        Console.WriteLine(log);
        Console.ForegroundColor = originalColor;
    }

    /// <summary>
    /// 空作用域，用于不需要实际作用域的场景
    /// </summary>
    class NullScope : IDisposable
    {
        /// <summary>
        /// 获取空作用域的单例实例
        /// </summary>
        public static NullScope Instance { get; } = new NullScope();

        NullScope() { }

        /// <summary>
        /// 释放资源，此方法不执行任何操作
        /// </summary>
        public void Dispose() { }
    }
}
