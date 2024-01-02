
using Google.Protobuf.Collections;
using JsonDb;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using System.Data;

namespace ProductAPI.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly Order.OrderClient _orderClient;
        private readonly IJsonDb db;

        public ProductController(ILogger<ProductController> logger, Order.OrderClient orderClient, IJsonDbFactory jsonDbFactory)
        {
            _logger = logger;
            _orderClient = orderClient;
            db = jsonDbFactory.GetJsonDb();
        }

        [HttpPost("PlaceOrder")]
        public IActionResult PlaceOrder(ProductOrderDto orderDetaildto)
        {
            var products = db.GetCollectionAsync<Product>("products").Result.Where(x => orderDetaildto.ProductQuantities.Exists(p => p.ProductId == x.Id));
            if (orderDetaildto != null && products.Any())
            {
                var productDetails = new RepeatedField<ProductDetails>();
                orderDetaildto.ProductQuantities.ForEach(x =>
                {

                    var product = products.FirstOrDefault(p => p.Id == x.ProductId);
                    if (product != null)
                    {
                        var pd = new ProductDetails();
                        pd.ProductId = product.Id.ToString();
                        pd.Description = product.Description;
                        pd.ProductTitle = product.Title;
                        pd.Price = product.Price;
                        pd.Quantity = x.Quantity;
                        productDetails.Add(pd);
                    }
                });
                var orderRequest = new OrderRequest
                {
                    OrderDetails = new OrderDetails()
                    {
                        OrderAddress = orderDetaildto.OrderAddress,
                    },
                };
                orderRequest.OrderDetails.ProductDetails.AddRange(productDetails);
                var reply = _orderClient.PlaceOrder(orderRequest);
                return Ok(new { order = reply.OrderDetails, Message = $"Product Order Placed with Id: {reply.OrderDetails.OrderId}" });
            }

            return BadRequest();

            return Ok();
        }

        //[HttpPost("UpdateOrder")]
        //public IActionResult UpdateOrder(int orderId, int productId, string shippingAddress, int quantity)
        //{
        //    var product = db.GetCollectionAsync<Product>("products").Result.SingleOrDefault(x => x.Id == productId);
        //    if (product != null)
        //    {
        //        var orderRequest = new OrderRequest
        //        {
        //            OrderDetails = new OrderDetails
        //            {
        //                OrderId = orderId,
        //                Color = product.color,
        //                Description = product.Description,
        //                OrderQuantity = quantity,
        //                ProductName = product.Name,
        //                ShipAddress = shippingAddress,
        //                UnitPrice = product.Price
        //            }
        //        };
        //        var reply = _orderProcessingClient.UpdateOrder(orderRequest);
        //        return Ok(new { order = reply.OrderDetails, Message = $"Order Updated" });
        //    }
        //    else
        //    {
        //        return BadRequest($"The Prodcut with ID:{productId} does not exist.");
        //    }
        //}

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await db.GetCollectionAsync<Product>("products");
            return products;
        }
    }
}
