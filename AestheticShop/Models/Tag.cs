namespace AestheticShop.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<ProductTag> ProductTags { get; set; }
    }
}
