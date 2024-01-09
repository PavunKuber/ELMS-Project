//Title:Employee Leave Management System Using ASP.NET
//Author:Pavun Kavitha
//Created At:(1/3/2023)
//Updated At:(10/6/2023)
//Reviewd By:Anitha
//Reviewd At:(19/6/2023)
using ELMSApplication;
using ELMSApplication.Filter;

using ELMSApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connection=builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddScoped<AuthorizationFilter>();
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
//builder.Services.AddScoped<LoggingFilter>();
//builder.Services.AddScoped<LoginResultFilter>();
builder.Services.AddScoped<ExceptionLogFilter>();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<ELMSApplicationContext>(options =>{
    options.UseSqlServer(connection);
});
var _logger=new LoggerConfiguration().
WriteTo.File("C:\\Users\\skart\\ELMSApplication\\ELMSApplication\\LOG\\Elms.log",rollingInterval:RollingInterval.Day).CreateLogger();
 builder.Logging.AddSerilog(_logger);



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Account/Login";
        option.Cookie.HttpOnly=true;
        option.Cookie.Name = "ELMSApplication";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(2);
        
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//  app.UseMvc(routes =>
//         {
//             //routes.MapRoute(
//             routes.MapRoute(
//                 name: "default",
//                 template: "{controller=TblRules}/{action=Index}/{id?}");
//         });

//app.UseCookiePolicy();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
