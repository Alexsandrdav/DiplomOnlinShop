namespace DiplomOnlineShop
{
    public class Product
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
 