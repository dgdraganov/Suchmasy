namespace Suchmasy.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public string SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public string Product { get; set; }
        public decimal PricePreUnit { get; set; }
        public string UnitOfMeasurment { get; set; }
        public decimal TotalPrice { get; set; }

        public string BuyerId { get; set; }
        public string BuyerName { get; set; }

    }
}
