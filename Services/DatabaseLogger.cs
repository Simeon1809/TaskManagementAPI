
using TaskManagementAPI.Models;
using TeamTaskManagementAPI.Data;

namespace TaskManagementAPI.Services
{
    public class DatabaseLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly IServiceScopeFactory _scopeFactory;

        public DatabaseLogger(string categoryName, IServiceScopeFactory scopeFactory)
        {
            _categoryName = categoryName;
            _scopeFactory = scopeFactory;
        }

        public IDisposable? BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) =>
            logLevel != LogLevel.None;

        public void Log<TState>(LogLevel logLevel, EventId eventId,
     TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel) || formatter == null)
                return;

            var message = formatter(state, exception);

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (!db.Database.CanConnect())
                {
                    Console.WriteLine("[Logger Warning] Skipped DB log: cannot connect.");
                    return;
                }

                db.Logs.Add(new LogEntry
                {
                    LogLevel = logLevel,
                    Message = message,
                    Exception = exception?.ToString(),
                    Source = _categoryName,
                    Timestamp = DateTime.UtcNow
                });

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Logger Error] Failed to log: {ex.Message}");
            }
        }

    }

}
