using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suchmasy.Data;
using Suchmasy.Models;
using Suchmasy.Repos.Contracts;

namespace Suchmasy.Pages
{
    public class CreateOrderModel : PageModel
    {
        public CreateOrderModel(UserManager<IdentityUser> userManager,
                                ApplicationDbContext dbContext,
                                IDeliveryRepository delivRepo)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _delivRepo = delivRepo;
        }

        public UserManager<IdentityUser> _userManager { get; }
        public ApplicationDbContext _dbContext { get; }
        public IDeliveryRepository _delivRepo { get; }


        [BindProperty(SupportsGet = true)]
        public string SupplierId { get; set; }
        public string SupplierBrandName { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string ProductId { get; set; }

        [BindProperty]
        public string RequestId { get; set; }
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
                RequestId = this.RequestId,
            };


            var request = _dbContext.Requests.FirstOrDefault(r => r.Id == order.RequestId);
            if (request == null)
            {
                TempData["ErrorMessages"] = new string[] { "Request Id is invalid!"};
                return LocalRedirect("/Orders");
            }
            else if (request.Status != RequestStatus.Submitted)
            {
                TempData["ErrorMessages"] = new string[] { "Request is not active!" };
                return LocalRedirect("/Orders");
            }
            request.Status = RequestStatus.Completed;
            request.ClosedByEmail = userEmail;
            request.ClosedOn = DateTime.Now;
            // TODO: Order repo
             _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            // TODO: create order 
            Delivery del = new Delivery(){
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                DestinationAddress = "Karnobat, ul Stara planina 5",
                DriverEmail = null,
                DriverId = null,
                OrderId = order.Id,
                DeliveredOn = new DateTime(),
                Status = DeliveryStatus.Generated
            };

            _delivRepo.SaveDelivery(del);

            _dbContext.SaveChanges();
            TempData["Success"] = true;
            return LocalRedirect("/Orders");
        }
    }
}
