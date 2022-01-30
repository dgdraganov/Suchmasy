using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Suchmasy.Data;

namespace Suchmasy.ViewComponents
{
    public class RequestsViewComponent : ViewComponent
    {

        public RequestsViewComponent(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ApplicationDbContext _dbContext { get; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var users = _dbContext.Requests.ToList();
            return View(users);
        }

    }
}
