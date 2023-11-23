using Microsoft.AspNetCore.Identity;

namespace AestheticShop.Models.Identity
{
    public class AppUser:IdentityUser<string>
    {
       public string FullName { get; set; }
       public int Age { get; set; }
    }
}
