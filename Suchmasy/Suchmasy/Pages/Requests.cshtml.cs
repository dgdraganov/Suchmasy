using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Suchmasy.Models;

namespace Suchmasy.Pages
{
    public class RequestsModel : PageModel
    {
        [BindProperty]
        public string SearchTerm { get; set; }
 
        public void OnGet()
        {
            var tempSucc = TempData["SuccessRequest"];
        }

        public void OnPost()
        {

        }
    }
}
