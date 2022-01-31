using Microsoft.AspNetCore.Mvc;
using Suchmasy.Models;

namespace Suchmasy.ViewComponents
{
    public class OrderCard : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Order orderCard)
        {
            return View(orderCard);
        }
    }
}
