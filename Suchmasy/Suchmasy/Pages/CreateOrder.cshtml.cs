using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suchmasy.Data;

namespace Suchmasy.Pages
{
    public class CreateOrderModel : PageModel
    {
        public CreateOrderModel(UserManager<IdentityUser> userManager,
                                ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public UserManager<IdentityUser> _userManager { get; }
        public ApplicationDbContext _context { get; }


        [BindProperty(SupportsGet = true)]
        public string SupplierId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ProductId { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal UnitPrice { get; set; }


        //public string UserEmail { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public void OnGet()
        {

        }
    }
}
