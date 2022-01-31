using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Suchmasy.Data;
using Suchmasy.Models;

namespace Suchmasy.ViewComponents
{
    public class OrdersViewComponent : ViewComponent
    {

        public OrdersViewComponent(ApplicationDbContext dbContext,
                                        UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        private ApplicationDbContext _dbContext { get; }
        private UserManager<IdentityUser> _userManager { get; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var requests = _dbContext.Orders.ToList();

            //TempData["UserId"] = _userManager.GetUserId(HttpContext.User);

            //var userId 
            //_dbContext.Requests.FirstOrDefault(p => p.RequesterId.Equals(userId));
            //TempData["CanCancelRequest"] = User.IsInRole("requester");
            return View(requests);
        }

    }
}
