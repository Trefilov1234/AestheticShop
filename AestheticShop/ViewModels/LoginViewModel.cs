﻿using System.ComponentModel.DataAnnotations;

namespace AestheticShop.ViewModels
{
	public class LoginViewModel
	{
		[Required]
		[MinLength(1)]
		[MaxLength(20)]
		public string Login { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(20)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
