namespace Suchmasy.Models
{
    public class Request
    {
        public string Id { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int UnitsNeeded { get; set; }

        public string RequesterId { get; set; }
        public string RequesterEmail { get; set; }

        public DateTime PlacedOn { get; set; }
    }
}
