﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Suchmasy.Data;
using Suchmasy.Models;

namespace Suchmasy.ViewComponents
{
    public class RequestsViewComponent : ViewComponent
    {

        public RequestsViewComponent(ApplicationDbContext dbContext,
                                        UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        private ApplicationDbContext _dbContext { get; }
        private UserManager<IdentityUser> _userManager { get; }

        public async Task<IViewComponentResult> InvokeAsync(RequestStatus status, string searchTerm)
        {
            List<Request> requests = null;
            if (searchTerm == null)
            {
                requests = _dbContext.Requests.Where(r => r.Status == status).ToList();
            }
            else
            {
                requests = _dbContext.Requests
                    .Where(r => r.Text.Contains(searchTerm) || 
                                r.Product.Contains(searchTerm))
                    .ToList();
            }
          
            TempData["UserId"] = _userManager.GetUserId(HttpContext.User);

            return View(requests);
        }
    }
}
