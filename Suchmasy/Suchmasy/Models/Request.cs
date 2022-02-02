namespace Suchmasy.Models
{
    public class Request
    {
        public string Id { get; set; }

        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Text { get; set; }

        public string? OrderId { get; set; }
        public Order Order { get; set; }

        public string RequesterId { get; set; }
        public string RequesterEmail { get; set; }

        public RequestStatus Status { get; set; }

        public string? ClosedById { get; set; }
        public string? ClosedByEmail { get; set; }
        public DateTime PlacedOn { get; set; }
        public DateTime ClosedOn { get; set; }
    }

    public enum RequestStatus
    {
        Submitted = 1,
        Completed = 2,
        Cancelled = 4
    }
}
