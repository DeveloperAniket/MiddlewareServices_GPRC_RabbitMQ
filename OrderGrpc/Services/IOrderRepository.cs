namespace OrderGrpc.Services
{
    public interface IOrderRepository
    {
        public Task<OrderDetails> PlaceOrder(OrderDetails orderDetails);
        public Task<OrderDetails> UpdateOrder(OrderDetails orderDetails);
    }
}
