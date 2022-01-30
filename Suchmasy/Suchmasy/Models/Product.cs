namespace Suchmasy.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }

    }
}
