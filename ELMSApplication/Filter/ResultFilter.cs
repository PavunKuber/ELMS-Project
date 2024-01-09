using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
using ELMSApplication.Models;
using ELMSApplication.Controllers;

namespace ELMSApplication.Filter;

public class LoginResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        // This method is called before the action result is executed

        // Check if the user is logged in
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            // User is not logged in, redirect to the login page
            Console.WriteLine("Result filter executing");
            context.Result = new RedirectToActionResult("Login", "Account", null);
        }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        // This method is called after the action result has been executed
        Console.WriteLine("Result filter executed");
            IActionResult result = context.Result;

        // Access the HTTP response
        HttpResponse response = context.HttpContext.Response;

        // Access the request context
        HttpContext httpContext = context.HttpContext;
        }
}