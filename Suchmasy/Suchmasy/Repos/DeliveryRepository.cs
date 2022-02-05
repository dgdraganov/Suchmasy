using Microsoft.AspNetCore.Identity;
using Suchmasy.Data;
using Suchmasy.Models;
using Suchmasy.Repos.Contracts;

namespace Suchmasy.Repos
{
    public class DeliveryRepository : IDeliveryRepository
    {
        public DeliveryRepository(ApplicationDbContext dbContext,
                                UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public ApplicationDbContext _dbContext { get; set; }
        public UserManager<IdentityUser> _userManager { get; set; }

        public Delivery GetDeliveryById(string id)
        {
            return _dbContext.Deliveries.FirstOrDefault(r => r.Id.Equals(id));
        }

        public IEnumerable<Delivery> GetAllDeliveries()
        {
            return _dbContext.Deliveries;
        }

        public bool SaveDelivery(Delivery newDelivery)
        {
            var del = GetDeliveryById(newDelivery.Id);
            if (del != null && del.Id.Equals(newDelivery.Id))
            {
                // Id already exists
                return false;
            }

            _dbContext.Deliveries.Add(newDelivery);
            _dbContext.SaveChanges();
            return true;
        }

        public bool SaveRequest(Request request)
        {
            throw new NotImplementedException();
        }

        public bool SetStatus(string delId, DeliveryStatus status)
        {
            // var del = _dbContext.Deliveries.FirstOrDefault(r => r.Id.Equals(delId));
            // if (del == null)
            //     return false;

            // del.Status = status;
            // if (status == DeliveryStatus.Accepted || DeliveryStatus.Delivered)
            // {
            //     del.DeliveredOn = DateTime.MinValue;
            // }
            throw new NotImplementedException("Auuuu");
        }

        public bool SetStatus(string reqId, string actionedById, RequestStatus status)
        {
            throw new NotImplementedException();
        }
    }
}