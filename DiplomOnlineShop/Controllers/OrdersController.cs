using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiplomOnlineShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OnlineShopContext dbContext;

        public OrdersController(ILogger<ProductsController> logger, OnlineShopContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            var orders = dbContext.Orders.Include(x => x.Products).ToList();

            return orders;
        }
        [HttpPut]
        public void Post(OrderModel orderModel)
        {
            var order = new Order();
            order.Email = orderModel.Email;
            order.Phone = orderModel.Phone;
            order.Date = DateTime.UtcNow;
            order.Products = new List<Product>();
            foreach (var id in orderModel.ProductIds)
            {
                var product = dbContext.Products.Single(x => x.Id == id);

                order.Products.Add(product);
            }


            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
        }

    }    
}