using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AestheticShop.Filters
{
    public class MyActionFilter: Attribute, IActionFilter
    {
        // public IUserManager userManager { get; set; }
        /* public MyActionFiltr(IUserManager userManager) 
         {
             this.userManager = userManager;
         }*/

        public string Message { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Cookies.Append("test", DateTime.Now.ToString());


        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                controller.ViewBag.FilterData = Message;
                context.HttpContext.Response.Cookies.Append("filter", DateTime.Now.ToString());
            }

        }
    }
}
