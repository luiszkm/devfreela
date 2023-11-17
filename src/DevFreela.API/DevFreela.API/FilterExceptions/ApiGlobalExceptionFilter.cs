using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevFreela.API.FilterExceptions;

public class ApiGlobalExceptionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // if (!context.ModelState.IsValid)
        // {
        //     var message = context.ModelState
        //         .SelectMany(ms => ms.Value.Errors)
        //         .Select(e => e.ErrorMessage)
        //         .ToList();
        //     context.Result = new BadRequestObjectResult(message);
        // }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}


