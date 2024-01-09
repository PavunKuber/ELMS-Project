//Title:Employee Leave Management System 
//Author:Pavun Kavitha
//Created At:(20/3/2023)
//Updated At:(10/6/2023)
//Reviewd By:Anitha
//Reviewd At:(19/6/2023)
using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ELMSApplication.Models;
using Serilog;

namespace ELMSApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IConfiguration configuration, ILogger<AccountController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("EmployeeId,Password")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionString = _configuration.GetConnectionString("DefaultConnection");

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand("SELECT * FROM Employee WHERE EmployeeId=@EmployeeId AND Password=@Password;", connection);
                        SqlParameter paramEmployeeId = new SqlParameter("@EmployeeId", employee.EmployeeId);
                        SqlParameter paramPassword = new SqlParameter("@Password", employee.Password);

                        command.Parameters.Add(paramEmployeeId);
                        command.Parameters.Add(paramPassword);

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                await reader.ReadAsync();

                                // Retrieve data from the reader
                                employee.Id = (long)reader.GetInt64(0);
                                employee.EmployeeName = reader.GetString(1);
                                employee.EmployeeId = reader.GetString(2);
                                employee.Password = reader.GetString(3);
                                employee.IsAdmin = reader.GetBoolean(4);

                                // Close the reader before exiting the method
                                reader.Close();

                                Log.Information("Employee Login Triggered");

                                HttpContext.Session.SetString("EmployeeName", employee.EmployeeName);
                                HttpContext.Session.SetString("EmployeeID", employee.EmployeeId);
                                HttpContext.Session.SetString("Admin", employee.IsAdmin.ToString());

                                List<Claim> claims = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId),
                                    new Claim(ClaimTypes.Role, employee.IsAdmin.ToString())
                                };

                                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                AuthenticationProperties properties = new AuthenticationProperties()
                                {
                                    AllowRefresh = true,
                                    IsPersistent = employee.KeepLoggedIn
                                };

                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                                return RedirectToAction("Index", "Leave");
                            }
                            else
                            {
                                ViewData["msg"] = "Invalid EmployeeId or password";
                                return View();
                            }
                        }
                    }
                }

                return View();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred during login");
                return View("Error");
            }
        }
    }
}
