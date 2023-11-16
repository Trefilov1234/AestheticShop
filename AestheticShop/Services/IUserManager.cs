using AestheticShop.Models;

namespace AestheticShop.Services
{
	public interface IUserManager
	{
		bool Login(string userName, string password);

		void GetUserCredentials();

		UserCredentials CurrentUser { get; set; }
	}
}
