using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Suchmasy.Models;
using Suchmasy.Repos.Contracts;

namespace Suchmasy.Pages
{
    public class DeliveriesModel : PageModel
    {
        public DeliveriesModel(IDeliveryRepository delivRepo)
        {
            _delivRepo = delivRepo;
        }
        public List<Delivery> Deliveries { get; set; }
        public IDeliveryRepository _delivRepo { get; }

        public void OnGet()
        {
            Deliveries = _delivRepo.GetAllDeliveries().ToList();
        }

        public void OnPost()
        {

        }
    }
}
