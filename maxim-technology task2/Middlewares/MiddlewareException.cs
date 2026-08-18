namespace maxim_technology_task2.Middlewares
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger _logger;

        public MiddlewareException(RequestDelegate requestDelegate, ILogger<MiddlewareException> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var start = DateTime.UtcNow;

            await _requestDelegate(context);

            var elapsed = DateTime.UtcNow - start;

            if (context.Response.StatusCode == 200)
            {
                _logger.LogInformation("Запрос {Method} {Path} выполнен успешно за {Time} мс", context.Request.Method, context.Request.Path, elapsed.TotalMilliseconds);
            }
            else if (context.Response.StatusCode == 400)
            {
                _logger.LogInformation("Ошибка запроса {Method} {Path}. Код: {StatusCode}", context.Request.Method, context.Request.Path, context.Response.StatusCode);
            } else if (context.Response.StatusCode == 503)
            {
                _logger.LogInformation("Веб-сервер временно не может обработать запрос из-за перегрузки или технических работ");
            }
        }
    }
}
