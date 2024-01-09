using ELMSApplication;
using ELMSApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connection=builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<ELMSApplicationContext>(options =>{
    options.UseSqlServer(connection);
});
var _logger=new LoggerConfiguration().
WriteTo.File("C:\\Users\\skart\\ELMSApplication\\Logger-.log",rollingInterval:RollingInterval.Day).CreateLogger();
 builder.Logging.AddSerilog(_logger);


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

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
