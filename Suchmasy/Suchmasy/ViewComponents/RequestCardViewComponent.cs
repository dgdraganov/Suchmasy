using Microsoft.AspNetCore.Mvc;
using Suchmasy.Models;

namespace Suchmasy.ViewComponents
{
    public class RequestCardViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Request requestCard)
        {
            return View(requestCard);
        }
    }
}
