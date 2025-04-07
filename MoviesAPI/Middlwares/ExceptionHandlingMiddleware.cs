using Repository;
using System.Diagnostics;

namespace MoviesAPI.Middlwares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var routeData = context.GetRouteData();
                var controller = routeData.Values["controller"]?.ToString();
                var action = routeData.Values["action"]?.ToString();

                var stackTrace = new StackTrace(ex, true); // true = get file info
                var frame = stackTrace.GetFrames()?.FirstOrDefault(f => f.GetFileLineNumber() > 0);
                var line = frame?.GetFileLineNumber();
                var file = frame?.GetFileName();

                LogException.LogIntoText(controller, action, line, file, ex);
                //LogException.LogIntoExcel(controller, action, line, file, ex);
                //LogException.LogIntoCsv(controller, action, line, file, ex);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong.");
            }
        }
    }

}
