namespace Suchmasy.Models.DTO
{
    public class DeliveryDTO
    {
        public string Id { get; set; }
        public string DestinationAddress { get; set; }         
        public string Status { get; set; }   
        public string OrderId { get; set; }   
        public string DriverEmail { get; set; }   
        public string CreatedOn { get; set; }         
        public string DeliveredOn { get; set; }         
    }
}
