using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Suchmasy.Data;
using Suchmasy.Models;
using Suchmasy.Repos.Contracts;

namespace Suchmasy.Repos
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext,
                                UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public ApplicationDbContext _dbContext { get; set; }
        public UserManager<IdentityUser> _userManager { get; set; }

        public IEnumerable<Order> GetAllOrders()
        {
            return _dbContext.Orders.Include(o => o.Product)
                                            .Include(o => o.Supplier);
        }

        public Order GetRequestById(string id)
        {
            return _dbContext.Orders
                        .FirstOrDefault(r => r.Id.Equals(id));
        }

        

        public bool SaveOrder(Order newOrder)
        {
            var req = GetRequestById(newOrder.Id);
            if(req != null && req.Id.Equals(newOrder.Id))
            {
                // Id already exists
                return false; 
            }

            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();
            return true;
        }


    }
}