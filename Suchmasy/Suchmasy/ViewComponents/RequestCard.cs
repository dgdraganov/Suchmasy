using Microsoft.AspNetCore.Mvc;
using Suchmasy.Models;

namespace Suchmasy.ViewComponents
{
    public class RequestCard : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Request requestCard)
        {
            return View(requestCard);
        }
    }
}
