using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Suchmasy.Models;
using Suchmasy.Repos.Contracts;

namespace Suchmasy.Pages
{
    public class DeliveryModel : PageModel
    {
        public DeliveryModel(IDeliveryRepository deliveryRepo,
                            UserManager<IdentityUser> userManager)
        {
            _deliveryRepo = deliveryRepo;
            _userManager = userManager;
        }

        public IDeliveryRepository _deliveryRepo { get; }
        public UserManager<IdentityUser> _userManager { get; }
        public Delivery Delivery { get; set; }
        public string UserId { get; set; }

        public void OnGet(string id)
        {
            Delivery = _deliveryRepo.GetDeliveryById(id);
            var user = _userManager.GetUserAsync(User).Result;

            if (user != null)
            {
                UserId = user.Id;
            }
        }

        public IActionResult OnPost(string delId, DeliveryStatus status)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var success = _deliveryRepo.SetStatus(delId, user.Id, status);
            if (!success)
            {
                // show error banner
            }
            // show success banner
            return Redirect("/Deliveries");
        }


    }
}
