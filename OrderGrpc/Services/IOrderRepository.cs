namespace OrderGrpc.Services
{
    public interface IOrderRepository
    {
        public OrderDetails PlaceOrder(OrderDetails orderDetails);
        public OrderDetails UpdateOrder(OrderDetails orderDetails);
    }
}
