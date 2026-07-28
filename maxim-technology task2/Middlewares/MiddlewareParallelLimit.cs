namespace maxim_technology_task2.Middlewares
{
    public class MiddlewareParallelLimit
    {
        private readonly RequestDelegate _next;
        private readonly SemaphoreSlim _semaphore;
        private readonly ILogger<MiddlewareParallelLimit> _logger;

        public MiddlewareParallelLimit(RequestDelegate next, IConfiguration configuration, ILogger<MiddlewareParallelLimit> logger)
        {
            _next = next;
            _logger = logger;

            string? configValue = configuration["Settings:ParallelLimit"];

            if (!int.TryParse(configValue, out int maxParallelRequests) || maxParallelRequests <= 0)
            {
                maxParallelRequests = 10;
                _logger.LogWarning("Ошибка при получении данных из конфигурации проекта");
            }

            _semaphore = new SemaphoreSlim(maxParallelRequests, maxParallelRequests);
        }

        public async Task InvokeAsync(HttpContext context)
        {   
            if (!await _semaphore.WaitAsync(0))
            {
                _logger.LogWarning("Превышен лимит параллельных запросов ({Limit}) на {Path}", _semaphore.CurrentCount, context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Сервер перегружен. Попробуйте позже");
                return;
            }

            try
            {
                await _next(context);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
