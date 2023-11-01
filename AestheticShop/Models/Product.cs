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
        //[MaxLength(50)]
        //[Required(ErrorMessage = "Введите категорию")]
        //[Display(Name = "Категория")]
        //public string Category { get; set; }
        [Display(Name = "Картинка")]
        [DataType(DataType.Upload)]
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Введите рейтинг")]
        [Display(Name = "Рейтинг")]
        public double Rating { get;set; }
        public int CategoryId { get;set; }
        
        public Category? Category { get; set; }
        public IEnumerable<ProductTag> ProductTags { get; set; }
    }
}
