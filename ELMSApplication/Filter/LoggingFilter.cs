using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Microsoft.Extensions.Logging;

public class LoggingFilter : ActionFilterAttribute
{
    private readonly ILogger<LoggingFilter> _logger;

    public LoggingFilter(ILogger<LoggingFilter> logger)
    {
        _logger = logger;
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {
        var controller = context.Controller as Controller;
        var actionName = context.ActionDescriptor.DisplayName;
        var result = context.Result;
        Console.WriteLine("Logging Filter executed");
        _logger.LogInformation("Action '{ActionName}' in controller '{ControllerName}' executed. Result: {@Result}", actionName, controller.GetType().Name, result);
    }
}

