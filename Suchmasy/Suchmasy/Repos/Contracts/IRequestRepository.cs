using Suchmasy.Models;

namespace Suchmasy.Repos.Contracts
{
    public interface IRequestRepository
    {
        bool SaveRequest(Request request);
        bool SetStatus(string reqId, string actionedById, RequestStatus status);
        Request GetRequestById(string id);
        bool CompleteRequest(string id, string userEmail);
    }
}