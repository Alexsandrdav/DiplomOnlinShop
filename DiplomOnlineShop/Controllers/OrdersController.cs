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
    }
}