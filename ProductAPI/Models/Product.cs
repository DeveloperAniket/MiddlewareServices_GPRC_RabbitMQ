namespace ProductAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }

        public int Stock { get; set; }

    }
}
