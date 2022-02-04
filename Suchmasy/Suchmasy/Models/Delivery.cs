namespace Suchmasy.Models
{
    public class Delivery
    {
        public string Id { get; set; }         
        public string DestinationAddress { get; set; }         
        public DeliveryStatus Status { get; set; }   
        public string OrderId { get; set; }   
        public Order Order { get; set; }   
        public string DriverId { get; set; }   
        public string DriverEmail { get; set; }   
        public DateTime CreatedOn { get; set; }         
        public DateTime DeliveredOn { get; set; }         
    }

    public enum DeliveryStatus{
        Generated,
        Accepted,
        Delivered
    }
}