using Microsoft.AspNetCore.Mvc;

namespace DiplomOnlineShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<ProductsController> _logger;
        private readonly OnlineShopContext dbContext;

        public ProductsController(ILogger<ProductsController> logger, OnlineShopContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var products = dbContext.Products.ToList();

            return products;
        }
    }
}