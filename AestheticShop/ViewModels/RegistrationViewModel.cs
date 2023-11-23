using System.ComponentModel.DataAnnotations;

namespace AestheticShop.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
        [Required]
        public string FullName { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
        public int Age { get; set; }
    }
}
