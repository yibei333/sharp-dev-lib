using Microsoft.Extensions.Logging;

namespace SharpDevLib;

internal class ConsoleLogger(string categoryName, LogLevel minLevel = LogLevel.Information) : ILogger
{
    private readonly string _categoryName = categoryName;
    private readonly LogLevel _minLevel = minLevel;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => NullScope.Instance;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _minLevel && logLevel != LogLevel.None;

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

    class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new NullScope();
        private NullScope() { }
        public void Dispose() { }
    }
}
