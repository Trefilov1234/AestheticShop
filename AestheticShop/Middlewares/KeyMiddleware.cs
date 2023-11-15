namespace AestheticShop.Middlewares
{
    public class KeyMiddleware
    {
        private RequestDelegate next;

        public KeyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            //  await context.Response.WriteAsync("goodbye");
            var key = context.Request.Query["key"];
            if (key == "qwerty")//key == "qwerty")
            {
                await next.Invoke(context);
            }
            else
            {
                await context.Response.WriteAsync("goodbye");
            }

        }
    }
}
