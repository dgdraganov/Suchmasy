using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Suchmasy.Data;
using Suchmasy.Models;
using Suchmasy.Repos;
using Suchmasy.Repos.Contracts;

namespace Suchmasy.Pages
{
    public class RequestsModel : PageModel
    {
        public RequestsModel(IRequestRepository requestRepo,
                                UserManager<IdentityUser> userManager)
        {
            _requestRepo = requestRepo;
            _userManager = userManager;
        }

        [BindProperty]
        public string SearchTerm { get; set; }
        public UserManager<IdentityUser> _userManager { get; }
        public IRequestRepository _requestRepo { get; }
 
        public void OnGet()
        {
            var tempSucc = TempData["SuccessRequest"];
        }

        public void OnPostDelete(string requestid)
        {
            var user = _userManager.GetUserAsync(User).Result;
            _requestRepo.SetStatus(requestid, user.Id, RequestStatus.Cancelled);

        }
    }
}
