using AestheticShop.Models;
using Microsoft.Extensions.Hosting;


namespace AestheticShop.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Product> RecentProducts { get; set; }
        public int CurrentPage { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? SelectedTagId { get; set; }
        public int TotalPages { get; set; }
        public int LimitPage { get; set; } = 2;
    }
}
