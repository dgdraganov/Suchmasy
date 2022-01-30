using Microsoft.AspNetCore.Mvc;
using Suchmasy.Data;

namespace Suchmasy.ViewComponents
{
    public class RequestViewComponent : ViewComponent
    {
        public RequestViewComponent(ApplicationDbContext dbContext)
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
