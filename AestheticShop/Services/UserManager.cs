using AestheticShop.Models;
using System.Text.Json;

namespace AestheticShop.Services
{
	public class UserManager : IUserManager
	{
		public UserCredentials CurrentUser { get; set; }

		private readonly ShopDbContext _shopDbContext;
		private readonly IHttpContextAccessor httpContextAccessor;

		public UserManager(ShopDbContext userDbContext, IHttpContextAccessor httpContextAccessor)
		{
			_shopDbContext = userDbContext;
			this.httpContextAccessor = httpContextAccessor;
		}

		public void GetUserCredentials()
		{
			try
			{
				if (httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("auth"))
				{
					var hash = httpContextAccessor.HttpContext.Request.Cookies["auth"];

					var json = AesOperation.DecryptString("b14ca5898a4e4133bbce2ea2315a1916", hash);
					CurrentUser = JsonSerializer.Deserialize<UserCredentials>(json);
					if (CurrentUser.Expiration >= DateTime.Now)
					{
						Console.WriteLine("YES USER " + CurrentUser.Expiration.ToString());
						Console.WriteLine($"{CurrentUser.Login} = Login from GetUserCrededantials");
						//return CurrentUser;
					}
					else
					{
						CurrentUser = null;
                    }
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			//Console.WriteLine("NO USER");
			//return null;
		}

		public bool Login(string userName, string password)
		{
			//var passwordHash = SHA256Encriptor.Encript(password);

			//var user = _shopDbContext.Users.FirstOrDefault(u => u.Login == userName && u.PasswordHash == passwordHash);
			//if (user != null)
			//{
			//	//  HttpContext.Response.Cookies.Append("auth", loginView.Login);
			//	UserCredentials userCrededantials = new UserCredentials()
			//	{
			//		Login = user.Login,
			//		IsAdmin = user.IsAdmin,
			//		Expiration = DateTime.Now + TimeSpan.FromMinutes(1)

			//	};
			//	var userCred = JsonSerializer.Serialize(userCrededantials);
			//	var hash = AesOperation.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", userCred);// шифруем куки
			//	httpContextAccessor.HttpContext.Response.Cookies.Append("auth", hash);
			//	return true;
			//}
			return false;
		}
	}
}
