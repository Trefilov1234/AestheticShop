using AestheticShop.Areas.Admin.Services;
using AestheticShop.Filters;
using AestheticShop.Middlewares;
using AestheticShop.Models;
using AestheticShop.Models.Identity;
using AestheticShop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<MailSenderService>();
builder.Services.AddDbContext<ShopDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ShopDbContext>().AddDefaultTokenProviders();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<AuthorizeFilter>();
builder.Services.AddControllersWithViews(o =>
{
    //o.Filters.Add<MyActionFilter>(); //будет работать при переходе на любую страницу
});
var app = builder.Build();
app.UseStaticFiles();
app.UseMiddleware<AuthMiddleware>();
//app.UseMiddleware<KeyMiddleware>();
//ShopDbInitializer.Seed(app);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}");
app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Product}/{action=Index}/{categoryId?}/{tagId?}");
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
