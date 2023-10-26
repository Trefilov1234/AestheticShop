using System.ComponentModel.DataAnnotations;

namespace AestheticShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Введите навзание")]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите описание")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Введите категорию")]
        [Display(Name = "Категория")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Введите ссылку на картинку")]
        [Display(Name = "Картинка")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Введите рейтинг")]
        [Display(Name = "Рейтинг")]
        public double Rating { get;set; }
    }
}
