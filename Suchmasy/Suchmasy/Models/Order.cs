namespace Suchmasy.Models
{
    public class Order
    {
        public string Id { get; set; }

        public string SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public string Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public string BuyerId { get; set; }
        public string BuyerEmail { get; set; }

        public DateTime PlacedOn { get; set; }

    }
}
