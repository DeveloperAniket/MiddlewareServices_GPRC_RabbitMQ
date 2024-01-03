using Grpc.Core;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using OrderGrpc.Services.RabbitMQ;

namespace OrderGrpc.Services
{
    public class OrderService : Order.OrderBase
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IRabbitMqService _rabbitMqService;

        public OrderService(ILogger<OrderService> logger, IOrderRepository orderRepository, IRabbitMqService rabbitMqService)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _rabbitMqService = rabbitMqService;
        }

        public override async Task<OrderResponse> PlaceOrder(OrderRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"GRPC method: {context.Method} recieved from :{context.Peer}. Host: {context.Host} ");
            var orderDetails = _orderRepository.PlaceOrder(request.OrderDetails);

            await _rabbitMqService.RaiseOrderCreate(orderDetails);

            // Send reply to GRPC Client
            return new OrderResponse
            {
                OrderDetails = orderDetails
            };
        }

        public override async Task<OrderResponse> UpdateOrder(OrderRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"RPC method: {context.Method} recieved from :{context.Peer}. Host: {context.Host} ");
            var orderDetails = _orderRepository.UpdateOrder(request.OrderDetails);

            await _rabbitMqService.RaiseOrderUpdate(orderDetails);
            
            // Send reply to GRPC Client
            return new OrderResponse
            {
                OrderDetails = orderDetails
            };
        }
    }
}
