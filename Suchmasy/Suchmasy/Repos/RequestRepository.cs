using Microsoft.AspNetCore.Identity;
using Suchmasy.Data;
using Suchmasy.Models;

namespace Suchmasy.Repos
{
    public class RequestRepository
    {
        public RequestRepository(ApplicationDbContext dbContext,
                                UserManager<IdentityUser> userManager
                                )
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public ApplicationDbContext _dbContext { get; set; }
        public UserManager<IdentityUser> _userManager { get; set; }

        public Request GetRequestById(string id)
        {
            return _dbContext.Requests.FirstOrDefault(r => r.Id.Equals(id));
        }

        public bool SaveRequest(Request newRequest)
        {
            var req = GetRequestById(newRequest.Id);
            if(req.Id.Equals(newRequest.Id))
            {
                // Id already exists
                return false; 
            }

            _dbContext.Requests.Add(newRequest);
            _dbContext.SaveChanges();
            return true;
        }

        public bool SetStatus(string reqId, string actionedById, RequestStatus status)
        {
            var req = _dbContext.Requests.FirstOrDefault(r => r.Id.Equals(reqId));
            if (req == null)
                return false;
            
            req.Status = status;
            if (status == RequestStatus.Submitted)
            {
                req.ClosedOn = DateTime.MinValue;
                req.ClosedById = null;
                req.ClosedByEmail = null;
            }
            else
            {
                var executingUser = _userManager.FindByIdAsync(actionedById).Result;
                req.ClosedOn = DateTime.Now;
                req.ClosedById = executingUser.Id;
                req.ClosedByEmail = executingUser.Email;
            }
            _dbContext.SaveChanges();
            return true;
        }
    }
}