using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MoviesAPI.Filters
{
    public class LogExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<LogExceptionFilter> _logger;

        public LogExceptionFilter(ILogger<LogExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();
            var ex = context.Exception;

            var stackTrace = new StackTrace(ex, true);
            var frame = stackTrace.GetFrames()?.FirstOrDefault(f => f.GetFileLineNumber() > 0);
            var line = frame?.GetFileLineNumber();
            var file = frame?.GetFileName();

            _logger.LogError(ex, "Exception in Controller: {Controller}, Action: {Action}, File: {File}, Line: {Line}",
                controller, action, file, line);
        }
    }
}
