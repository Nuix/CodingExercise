using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace InvestmentPerformanceWebApi.Test.Spys;

internal class SpyLogger<T> : ILogger<T>
{
    public List<LogInfo> Logs { get; set; } = new List<LogInfo>();
    public object Scope { get; set; } = new object();
    public bool LoggerWasCalled { get; set; }

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    public void Log<TState>(LogLevel logLevel, EventId enventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        Logs.Add(new LogInfo
        {
            LogLevel = logLevel,
            Ex = exception,
            Message = state?.ToString() ?? "",
            Scope = Scope
        });
        LoggerWasCalled = true;
    }
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).

    public void ClearLogs()
    {
        LoggerWasCalled = false;
        Logs.Clear();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8601 // Possible null reference assignment.
    public IDisposable BeginScope<TState>(TState state)
    {
        Scope = state;
        return null;
    }
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8601 // Possible null reference assignment.

    internal class LogInfo
    {
        public LogLevel LogLevel { get; set; }
        public EventId EventId { get; set; }
        public string? Message { get; set; }
        public Exception? Ex { get; set; }
        public object? Scope { get; set; }
    }
}
