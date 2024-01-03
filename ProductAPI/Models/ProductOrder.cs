namespace ProductAPI.Models
{
    public class ProductOrderDto
    {
        public int? OrderId { get; set; }
        public string OrderAddress { get; set; }
        public List<ProductQuantityDto> ProductQuantities { get; set; }
    }

    public class ProductQuantityDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}