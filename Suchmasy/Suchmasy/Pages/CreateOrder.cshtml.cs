using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suchmasy.Data;
using Suchmasy.Models;

namespace Suchmasy.Pages
{
    public class CreateOrderModel : PageModel
    {
        public CreateOrderModel(UserManager<IdentityUser> userManager,
                                ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public UserManager<IdentityUser> _userManager { get; }
        public ApplicationDbContext _dbContext { get; }


        [BindProperty(SupportsGet = true)]
        public string SupplierId { get; set; }
        public string SupplierBrandName { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string ProductId { get; set; }
        public string ProductName { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal UnitPrice { get; set; }

        public string UserEmail { get; set; }


        [BindProperty]
        public int Quantity { get; set; }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            UserEmail = await _userManager.GetEmailAsync(user);

            ProductName = _dbContext.Products.FirstOrDefault(p => p.Id == ProductId).ProductName;
            SupplierBrandName = _dbContext.Suppliers.FirstOrDefault(s => s.Id == SupplierId).BrandName;

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["Success"] = false;
                TempData["ErrorMessages"] = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage).ToList();
                return Redirect("/Orders");
            }

            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);
            var userEmail = await _userManager.GetEmailAsync(user);

            Order order = new Order(){
                Id = Guid.NewGuid().ToString(),
                SupplierId = this.SupplierId,
                ProductId = this.ProductId,
                UnitPrice = UnitPrice,
                Quantity = this.Quantity,
                TotalPrice = UnitPrice * this.Quantity,
                BuyerId = userId,
                BuyerEmail = userEmail,
                PlacedOn = DateTime.Now,
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            TempData["Success"] = true;
            return LocalRedirect("/Orders");
        }
    }
}
