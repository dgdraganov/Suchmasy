using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Suchmasy.Pages
{
    // [Authorize(Roles = "adminnn")]
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly RoleManager<IdentityRole> _rManager;
        private readonly UserManager<IdentityUser> uManager;
        private readonly IAuthorizationService authService;

        //private readonly RoleClaimManager<IdentityRole> _rManager;

        public PrivacyModel(ILogger<PrivacyModel> logger, 
            RoleManager<IdentityRole> rManager,
            UserManager<IdentityUser> uManager,
            IAuthorizationService authService)
        {
            _logger = logger;
            this._rManager = rManager;
            this.uManager = uManager;
            this.authService = authService;
        }

   
        public async Task OnGet()
        {
            ///var user = await uManager.GetUserAsync(HttpContext.User);
            var claims = HttpContext.User.Claims;
        }
    }
}


//var user = await uManager.GetUserAsync(HttpContext.User);

//var users = await uManager.GetUsersInRoleAsync("adminnn");
//var auth = await authService.AuthorizeAsync(User, "full_access");

// ======================================

//var user = await uManager.GetUserAsync(HttpContext.User);
//if (user != null)
//{
//    var identityResult = await uManager.AddToRoleAsync(user, "adminnn");
//    if (identityResult != null && identityResult.Succeeded)
//    {
//        Console.WriteLine("Yesssssss");
//    }
//}


// ============================


//IdentityRole ir = new IdentityRole("adminnn");
//IdentityResult result = _rManager.CreateAsync(ir).Result;
//if (result.Succeeded)
//{
//    Console.WriteLine("Added to db");
//}

//var role = _rManager.Roles.First(r => r.Name == "adminnn");

//var claim = new Claim("Permission", "view");
//var res = await _rManager.AddClaimAsync(role, claim);
//if (res != null && res.Succeeded)
//{
//    Console.WriteLine("view");
//}
//claim = new Claim("Permission", "delete");
//res = await _rManager.AddClaimAsync(role, claim);
//if (res != null && res.Succeeded)
//{
//    Console.WriteLine("delete");
//}
//claim = new Claim("Permission", "edit");
//res = await _rManager.AddClaimAsync(role, claim);
//if (res != null && res.Succeeded)
//{
//    Console.WriteLine("edit");
//}
