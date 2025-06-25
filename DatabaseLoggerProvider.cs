using TaskManagementAPI.Services;

namespace TaskManagementAPI
{
    public class DatabaseLoggerProvider : ILoggerProvider
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DatabaseLoggerProvider(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(categoryName, _scopeFactory);
        }

        public void Dispose() { }
    }

}
