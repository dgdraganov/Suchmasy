using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Suchmasy.Models.DTO;
using Suchmasy.Repos.Contracts;

namespace Suchmasy.Pages
{
    public class DeliveriesModel : PageModel
    {
        public DeliveriesModel(IDeliveryRepository delivRepo)
        {
            _delivRepo = delivRepo;
        }
        public List<DeliveryDTO> Deliveries { get; set; }
        public IDeliveryRepository _delivRepo { get; }

        public void OnGet()
        {
            var deliveries = _delivRepo.GetAllDeliveries().ToList();
            Deliveries = new List<DeliveryDTO>();
            foreach (var del in deliveries)
            {
                Deliveries.Add(new DeliveryDTO()
                {
                    Id = del.Id,
                    DestinationAddress = del.DestinationAddress,
                    Status = del.Status.ToString(),
                    OrderId = del.OrderId,
                    DriverEmail = del.DriverEmail ?? "-",
                    CreatedOn = del.CreatedOn.ToString(),
                    DeliveredOn = del.DeliveredOn == new DateTime() ? "-" : del.DeliveredOn.ToString(),
                });
            }
            Deliveries = Deliveries.OrderByDescending(d => d.Status).ToList();
        }

        public void OnPost()
        {

        }
    }
}
