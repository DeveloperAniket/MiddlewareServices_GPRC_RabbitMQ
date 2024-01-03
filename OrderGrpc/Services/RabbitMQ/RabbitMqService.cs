using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace OrderGrpc.Services.RabbitMQ
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnection _connection;

        public RabbitMqService()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
        }

        public async Task RaiseOrderCreate(OrderDetails orderDetails)
        {
            // Connect to RabbitMQ and push the message to exchange
            using var model = _connection.CreateModel();
            model.QueueDeclare("OrderCreationQueue1", durable: true, exclusive: false, autoDelete: false);
            model.QueueDeclare("OrderCreationQueue2", durable: true, exclusive: false, autoDelete: false);
            model.ExchangeDeclare("FanoutExchange", ExchangeType.Fanout, durable: true, autoDelete: false);
            model.QueueBind("OrderCreationQueue1", "FanoutExchange", string.Empty);
            model.QueueBind("OrderCreationQueue2", "FanoutExchange", string.Empty);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(orderDetails));
            model.BasicPublish("FanoutExchange", string.Empty, null, body);

            model.Close();

            await Task.CompletedTask;
        }

        public async Task RaiseOrderUpdate(OrderDetails orderDetails)
        {
            // Connect to RabbitMQ and push the message to exchange
            using var model = _connection.CreateModel();
            model.QueueDeclare("OrderUpdateQueue", durable: true, exclusive: false, autoDelete: false);
            model.ExchangeDeclare("TopicExchange", ExchangeType.Topic, durable: true, autoDelete: false);
            model.QueueBind("OrderUpdateQueue", "TopicExchange", "OrderUpdated");

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(orderDetails));
            model.BasicPublish("TopicExchange", "OrderUpdated", null, body);

            model.Close();
            await Task.CompletedTask;
        }
    }
}
