using RabbitMQ.Client;

namespace OrderGrpc.Services.RabbitMQ
{
    public interface IRabbitMqService
    {
        Task RaiseOrderCreate(OrderDetails orderDetails);
    }
}
