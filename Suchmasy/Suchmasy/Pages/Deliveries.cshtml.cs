using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Suchmasy.Models;

namespace Suchmasy.Pages
{
    public class DeliveriesModel : PageModel
    {

        public List<Delivery> Deliveries { get; set; }
        public void OnGet()
        {
            Deliveries = new List<Delivery>(){
                new Delivery(){
                    Id = Guid.NewGuid().ToString(),
                    CreatedOn = new DateTime(),
                    DriverEmail = "mikko",
                    Status = DeliveryStatus.Generated,
                    DestinationAddress = "sadsadadsadsadsadsad",
                },
                 new Delivery(){
                    Id = Guid.NewGuid().ToString(),
                    CreatedOn = new DateTime(),
                    DeliveredOn = DateTime.Now,
                    DriverEmail = "penkko",
                    Status = DeliveryStatus.Delivered,
                    DestinationAddress = "sadsadadsadsadsadsad",
                },
            };
        }

        public void OnPost()
        {

        }
    }
}
