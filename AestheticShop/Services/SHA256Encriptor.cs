using System.Security.Cryptography;
using System.Text;

namespace AestheticShop.Services
{
	public static class SHA256Encriptor
	{
		public static string Encript(string str)
		{
			using (SHA256 sHA = SHA256.Create())
			{
				byte[] bytes = sHA.ComputeHash(Encoding.UTF8.GetBytes(str));
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					stringBuilder.Append(bytes[i].ToString("x2"));
				}

				return stringBuilder.ToString();

			}
		}
	}
}
