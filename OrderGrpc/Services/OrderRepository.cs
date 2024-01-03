using JsonDb;

namespace OrderGrpc.Services
{
    public class OrderRepository : IOrderRepository
    {

        private readonly IJsonDb db;

        public OrderRepository(IJsonDbFactory jsonDbFactory)
        {
            db = jsonDbFactory.GetJsonDb();
        }
        public async Task<OrderDetails> PlaceOrder(OrderDetails orderDetails)
        {
            orderDetails.OrderId = Guid.NewGuid().ToString();

            var orders = await db.GetCollectionAsync<OrderModel>("orders");

            var om = new OrderModel()
            {

                OrderId = orderDetails.OrderId,
                OrderAddress = orderDetails.OrderAddress,
                Products = new List<OrderProductModel>()
            };

            foreach (var item in orderDetails.ProductDetails)
            {
                var opm = new OrderProductModel();
                opm.ProductPrice = item.Price;
                opm.ProductId = item.ProductId;
                opm.ProductCount = item.Quantity;

                om.Products.Add(opm);
            }


            orders.Add(om);
            await orders.WriteAsync();
            return orderDetails;
        }

        public async Task<OrderDetails?> UpdateOrder(OrderDetails orderDetails)
        {
            var orders = await db.GetCollectionAsync<OrderModel>("orders");

            var matched = orders.Where(x => x.OrderId == orderDetails.OrderId).FirstOrDefault();

            if (matched != null)
            {
                matched.OrderId = orderDetails.OrderId;
                matched.OrderAddress = orderDetails.OrderAddress;
                matched.Products = new List<OrderProductModel>();
                foreach (var item in orderDetails.ProductDetails)
                {
                    var opm = new OrderProductModel();
                    opm.ProductPrice = item.Price;
                    opm.ProductId = item.ProductId;
                    opm.ProductCount = item.Quantity;

                    matched.Products.Add(opm);
                }
                await orders.WriteAsync();
                return orderDetails;
            }
            else
            {
                return null;
            }           
        }
    }
}
