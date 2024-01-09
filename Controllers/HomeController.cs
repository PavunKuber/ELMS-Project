using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ELMSApplication.Models;
using ELMSApplication.Filters;
namespace ELMSApplication.Controllers;
[ExceptionLogFilter]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        //int a=67;
        //int b=0;
        //int c=a/b;
        return View();
    }

    public IActionResult Privacy()
    {  
       // throw new Exception("Testing Error");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
