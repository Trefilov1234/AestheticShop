using AestheticShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AestheticShop.Filters
{
    public class MyAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        //  public IUserManager userManager  { get; set; }

        /* public AuthorizeFilter(IUserManager userManager)
         {
             this.userManager = userManager;
         }*/
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            IUserManager userManager = context.HttpContext.RequestServices.GetRequiredService<IUserManager>();

            if (userManager.CurrentUser == null)
            {
                context.Result = new RedirectToActionResult("Error", "Home", null);
            }
        }
    }
}
