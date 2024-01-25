namespace DiplomOnlineShop
{
    public class OrderModel
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public IList<int> ProductIds { get; set; }
    }
}

