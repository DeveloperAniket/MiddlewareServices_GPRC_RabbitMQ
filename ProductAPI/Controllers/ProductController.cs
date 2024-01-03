
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using JsonDb;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using System.Data;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("product")]
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

        [HttpPost("order/create")]
        public IActionResult PlaceOrder(ProductOrderDto orderDetaildto)
        {
            if (orderDetaildto == null || string.IsNullOrEmpty(orderDetaildto.OrderAddress.Trim()) || !orderDetaildto.ProductQuantities.Any())
            {
                return BadRequest("OrderAddress or Product Details missing");
            }


            var products = db.GetCollectionAsync<Product>("products").Result.Where(x => orderDetaildto.ProductQuantities.Exists(p => p.ProductId == x.Id));
            if (products.Any())
            {
                var productDetails = new RepeatedField<ProductDetails>();
                orderDetaildto.ProductQuantities.ForEach(x =>
                {

                    var product = products.FirstOrDefault(p => p.Id == x.ProductId);
                    if (product != null && x.Quantity > 0)
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
                if (productDetails.Any())
                {
                    var orderRequest = new OrderRequest
                    {
                        OrderDetails = new OrderDetails()
                        {
                            OrderAddress = orderDetaildto.OrderAddress,
                        },
                    };
                    orderRequest.OrderDetails.ProductDetails.AddRange(productDetails);
                    OrderResponse reply;
                    try
                    {
                        reply = _orderClient.PlaceOrder(orderRequest);
                        return Ok(new { order = reply.OrderDetails, Message = $"Product Order Placed with Id: {reply.OrderDetails.OrderId}" });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "OrderClient Server Issue");
                        throw new Exception("OrderClient Server Issue");
                    }
                }
                else
                {
                    return BadRequest($"No Product found to place order.");
                }

            }
            else
            {
                return BadRequest("Product not found");
            }
        }

        [HttpPost("order/update")]
        public IActionResult UpdateOrder(ProductOrderDto orderDetaildto)
        {
            if (orderDetaildto == null || orderDetaildto.OrderId is null or < 1 || string.IsNullOrEmpty(orderDetaildto.OrderAddress.Trim()) || !orderDetaildto.ProductQuantities.Any())
            {
                return BadRequest("OrderId or OrderAddress or Product Details missing");
            }

            var products = db.GetCollectionAsync<Product>("products").Result.Where(x => orderDetaildto.ProductQuantities.Exists(p => p.ProductId == x.Id));
            if (products.Any())
            {
                var productDetails = new RepeatedField<ProductDetails>();
                orderDetaildto.ProductQuantities.ForEach(x =>
                {
                    var product = products.FirstOrDefault(p => p.Id == x.ProductId);
                    if (product != null && x.Quantity > 0)
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

                if (productDetails.Any())
                {
                    var orderRequest = new OrderRequest
                    {
                        OrderDetails = new OrderDetails()
                        {
                            OrderId = orderDetaildto.OrderId.Value,
                            OrderAddress = orderDetaildto.OrderAddress,
                        },
                    };
                    orderRequest.OrderDetails.ProductDetails.AddRange(productDetails);

                    OrderResponse reply;
                    try
                    {
                        reply = _orderClient.UpdateOrder(orderRequest);
                        return Ok(new { order = reply.OrderDetails, Message = $"Product Order updated. OrderId: {reply.OrderDetails.OrderId}" });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "OrderClient Server Issue");
                        throw new Exception("OrderClient Server Issue");
                    }
                }
                else
                {
                    return BadRequest($"No Product found to place order.");
                }
            }
            else
            {
                return BadRequest("Product not found");
            }
        }

        [HttpGet("products")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await db.GetCollectionAsync<Product>("products");
            return products;
        }
         
        
    }
}
