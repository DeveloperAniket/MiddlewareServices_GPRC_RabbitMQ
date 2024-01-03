namespace OrderGrpc.Services
{
    public class OrderModel
    {
        public string OrderId { get; set; }
        public string OrderAddress { get; set; }
        
        public List<OrderProductModel> Products { get; set; }   
    }

    public class OrderProductModel
    {
        public string ProductId { get; set; }

        public int ProductCount { get; set; }

        public double ProductPrice { get; set; }
    }
}