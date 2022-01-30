namespace Suchmasy.Models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; }
        public string Product { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Enabled { get; set; }
        public int PaymentTerms { get; set; }
    }
}
