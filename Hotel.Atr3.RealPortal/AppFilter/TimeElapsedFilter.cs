using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Hotel.Atr3.RealPortal.AppFilter
{
    public class TimeElapsedFilter : Attribute, IActionFilter
    {
        private readonly ILogger<TimeElapsedFilter> _logger;
        private Stopwatch timer;
        public TimeElapsedFilter(ILogger<TimeElapsedFilter> logger)
        {
            _logger = logger;
        }

        //сразу после завершения этапа [Stage]
        public void OnActionExecuted(ActionExecutedContext context)
        {
            timer.Stop();

            string path = context.HttpContext.Request.Path;
            var ms = timer.Elapsed.TotalMilliseconds;

            _logger.LogInformation("Запрос для метода {path} отработал за {TotalMilliseconds}",
                path, ms);
        }

        //вызывается непосредственно перед этапом Stage
        public void OnActionExecuting(ActionExecutingContext context)
        {
            timer = Stopwatch.StartNew();
        }
    }
}
