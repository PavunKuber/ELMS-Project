// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
// using Microsoft.EntityFrameworkCore;
// using ELMSApplication.Models;
// namespace ELMSApplication.Filter
// {
//     public class AuthorizationFilter:IAsyncAuthorizationFilter{
    
//     private readonly ELMSApplicationContext _context;
//     public AuthorizationFilter(ELMSApplicationContext context){
//         _context=context;
//     }

//         public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
//         {
//       if(!(await IsAdminAuthenticated(context))){
//         context.Result=new UnauthorizedResult();
//       }
//     }

//     //     public async Task OnAuthorizationFilter(AuthorizationFilterContext context){
//     //   if(!(await IsAdminAuthenticated(context))){
//     //     context.Result=new UnauthorizedResult();
//     //   }
//     // }
//     private async Task<bool> IsAdminAuthenticated(AuthorizationFilterContext context){
//         if(context.HttpContext.User.Identity.IsAuthenticated){
//             var EmployeeId = context.HttpContext.User.Identity.Name;
//             var Password = context.HttpContext.Request.Form["Password"];
//             //var user != null;
//         }
//         return false;
//     }
// }
// }