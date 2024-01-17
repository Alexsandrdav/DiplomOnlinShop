namespace DiplomOnlineShop
{
    public class Order
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Date { get; set; }

        public int Id { get; set; }

        public IList<Product> Products { get; set; }
    }
}
 