using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suchmasy.Data;
using Suchmasy.Models;
using Suchmasy.Models.DTO;

namespace Suchmasy.Pages
{
    public class CreateRequestModel : PageModel
    {

        public CreateRequestModel(UserManager<IdentityUser> userManager, 
                                  ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public UserManager<IdentityUser> _userManager { get; }
        public ApplicationDbContext _context { get; }

        public string UserEmail { get; set; }

        [BindProperty]
        public string Product { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        [BindProperty]
        public string Description { get; set; }

        public void OnGet()
        {
            var user = _userManager.GetUserAsync(User).Result;
            UserEmail = user.Email;
        }

        public IActionResult OnPost(/*RequestModel request*/)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (!ModelState.IsValid)
            {
                TempData["Success"] = false;

                TempData["ErrorMessages"] = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage).ToList();

                return Redirect("/Requests");
            }
            Request newReq = new Request()
            {
                Id = Guid.NewGuid().ToString(),
                Product = this.Product,
                Quantity = this.Quantity,
                Text = this.Description,
                RequesterId = user.Id,
                RequesterEmail = user.Email,
                PlacedOn = DateTime.Now
            };


            _context.Add(newReq);
            _context.SaveChanges();
            TempData["Success"] = true;
            return Redirect("/Requests");
        }
    }
}
