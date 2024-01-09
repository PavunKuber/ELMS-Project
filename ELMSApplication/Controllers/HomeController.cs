using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using ELMSApplication.Filter;
using ELMSApplication.Models;
using System.Diagnostics;

namespace ELMSApplication.Controllers;
[ServiceFilter(typeof(ExceptionLogFilter))]
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
        // using (TcpClient client = new TcpClient())
        //     {
        //         // Perform socket operations
        //         client.Connect("127.0.0.1", 8080);
                
        //         // Send and receive data through the socket
        //         // ...
        //     }
        return View();
    }

    public IActionResult Privacy()
    {        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
