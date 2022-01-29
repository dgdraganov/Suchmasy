namespace Suchmasy.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public string Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public string BuyerId { get; set; }
        public string BuyerName { get; set; }

        public DateTime PlacedOn { get; set; }

    }
}
