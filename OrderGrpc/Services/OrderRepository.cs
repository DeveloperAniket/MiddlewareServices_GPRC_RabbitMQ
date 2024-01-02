namespace OrderGrpc.Services
{
    public class OrderRepository : IOrderRepository
    {
        public OrderDetails PlaceOrder(OrderDetails orderDetails)
        {
            orderDetails.OrderId = new Random().Next(10, 10000);
            return orderDetails;
        }

        public OrderDetails UpdateOrder(OrderDetails orderDetails)
        {
            return orderDetails;
        }
    }
}
