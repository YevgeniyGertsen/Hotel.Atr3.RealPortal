using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hotel.Atr3.RealPortal.AppFilter
{
    public class IEFilter : Attribute, IResourceFilter
    {
        //сразу после завершения этапа [Stage]
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            throw new NotImplementedException();
        }

        //вызывается непосредственно перед этапом Stage
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36
            string userAgent = context.HttpContext
                                      .Request
                                      .Headers["user-agent"].ToString();

            if(userAgent.Contains("Mozilla"))
            {
                context.Result = new ContentResult()
                {
                    Content = "Ваш браузер устарел!"
                };
            }
        }
    }
}
