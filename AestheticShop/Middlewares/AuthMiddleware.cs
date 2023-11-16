using AestheticShop.Services;

namespace AestheticShop.Middlewares
{
	public class AuthMiddleware
	{
		private RequestDelegate next;
		//private IUserManager userManager;

		public AuthMiddleware(RequestDelegate next)
		{
			this.next = next;
			//	this.userManager = userManager;
		}
		public async Task InvokeAsync(HttpContext context, IUserManager userManager)
		{

			//userManager = context.RequestServices.GetRequiredService<IUserManager>();
			userManager.GetUserCredentials();

			await next.Invoke(context);

			//userManager = context.RequestServices.GetRequiredService<IUserManager>();
			//var userCrededantials = userManager.GetUserCredentials();

			//if (userCrededantials != null || true)
			//{
			//	await next.Invoke(context);
			//}
			//else
			//{
			//	await context.Response.WriteAsync("Unauthorization!!!!");
			//}


		}

	}
}
