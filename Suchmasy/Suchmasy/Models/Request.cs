namespace Suchmasy.Models
{
    public class Request
    {
        public string Id { get; set; }

        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Text { get; set; }

        public string RequesterId { get; set; }
        public string RequesterEmail { get; set; }
        public bool Completed { get; set; }

        public DateTime PlacedOn { get; set; }
    }
}
