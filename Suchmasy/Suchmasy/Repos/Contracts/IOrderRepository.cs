using Suchmasy.Models;

namespace Suchmasy.Repos.Contracts
{
    public interface IOrderRepository
    {
        bool SaveOrder(Order request);
        Order GetRequestById(string id);
        IEnumerable<Order> GetAllOrders();
    }
}