using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tazkarti.API.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        this.logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception.Message, "Unhandled exception occurred");

        var response = new
        {
            Messaege = "Something went wrong, please try again"
        };
        
        context.Result = new JsonResult(response)
        {
            StatusCode = 500
        };
        
        context.ExceptionHandled = true;
    }
}