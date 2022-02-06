using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Suchmasy.Models;
using Suchmasy.Repos.Contracts;

namespace Suchmasy.Pages
{
    public class DeliveryModel : PageModel
    {
        public DeliveryModel(IDeliveryRepository deliveryRepo)
        {
            _deliveryRepo = deliveryRepo;
        }

        public IDeliveryRepository _deliveryRepo { get; }
        public Delivery Delivery { get; set; }
        
        public void OnGet(string id)
        {

            Delivery = _deliveryRepo.GetDeliveryById(id);

        }

      
    }
}
