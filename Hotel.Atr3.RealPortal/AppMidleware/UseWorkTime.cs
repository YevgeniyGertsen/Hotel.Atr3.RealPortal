using Twilio.Rest.Api.V2010.Account.Usage.Record;

namespace Hotel.Atr3.RealPortal.AppMidleware
{
    public class UseWorkTime
    {
        private readonly RequestDelegate _next;

        public UseWorkTime(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentHour = DateTime.Now.Hour;
            if(currentHour>= 9 && currentHour< 12)
            {
                await context.Response
                    .WriteAsync("Сайт в это время не работает");
            }

            await _next.Invoke(context);
        }
    }

    public static class UseWorkTimeExtensions
    {
        public static IApplicationBuilder UseWorkTime
            (this IApplicationBuilder app)
        {
            return app.UseMiddleware<UseWorkTime>();
        }
    }
}
