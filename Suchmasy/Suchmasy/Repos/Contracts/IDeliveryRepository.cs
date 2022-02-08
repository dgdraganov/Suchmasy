using Suchmasy.Models;

namespace Suchmasy.Repos.Contracts
{
    public interface IDeliveryRepository
    {
        bool SaveDelivery(Delivery request);
        bool SetStatus(string delId, string dviverId,DeliveryStatus status);
        Delivery GetDeliveryById(string id);
        IEnumerable<Delivery> GetAllDeliveries();

    }
}