using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suchmasy.Models;
using Suchmasy.Repos.Contracts;

namespace Suchmasy.Pages
{
    public class OrdersModel : PageModel
    {
        public OrdersModel(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public IOrderRepository _orderRepo { get; }
        public IEnumerable<Order> Orders { get; set; }

        public void OnGet()
        {
               Orders = _orderRepo.GetAllOrders().ToList();
        }
    }
}
