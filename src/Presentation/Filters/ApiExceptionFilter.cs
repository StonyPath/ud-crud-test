using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Presentation.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Log.Error(context.Exception, "Unhandled exception occurred.");
        var result = new ObjectResult(new
        {
            Message = "An unexpected error occurred.",
            Detail = context.Exception.Message
        });
        result.StatusCode = 500;
        context.Result = result;
    }
}