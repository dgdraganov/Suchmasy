using Microsoft.AspNetCore.Identity;
using Suchmasy.Data;
using Suchmasy.Models;
using Suchmasy.Repos.Contracts;

namespace Suchmasy.Repos
{
    public class RequestRepository : IRequestRepository
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

        public bool CompleteRequest(string id, string userEmail)
        {
            var request = _dbContext.Requests.FirstOrDefault(r => r.Id == id);
            if (request == null)
            {
                return false;
            }
            else if (request.Status != RequestStatus.Submitted)
            {
                return false;
            }

            request.Status = RequestStatus.Completed;
            request.ClosedByEmail = userEmail;
            request.ClosedOn = DateTime.Now;
            return true;
        }

        public Request GetRequestById(string id)
        {
            return _dbContext.Requests
                        .FirstOrDefault(r => r.Id.Equals(id));
        }

        public bool SaveRequest(Request newRequest)
        {
            var req = GetRequestById(newRequest.Id);
            if(req != null && req.Id.Equals(newRequest.Id))
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