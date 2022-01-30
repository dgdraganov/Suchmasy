using Microsoft.AspNetCore.Identity;
using Suchmasy.Data;
using Suchmasy.Models;
using System.Security.Claims;

namespace Suchmasy
{
    public class InitialData
    {

        public IServiceProvider _serviceProvider { get; set; }

        public InitialData(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Seed()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                seedUsersAndRoles(scope);
                seedSuppliersAndOrders(scope);
            }
        }

        private void seedUsersAndRoles(IServiceScope scope)
        {
            // ------ APPLICATION DB CONTEXT ------
            var dbContext = (ApplicationDbContext?)scope
                                           .ServiceProvider
                                           .GetService(typeof(ApplicationDbContext));
            if (dbContext == null)
                throw new Exception($"Can not resolve {typeof(ApplicationDbContext)}");

            dbContext.Database.EnsureCreated();


            // ------ USER MANAGER ------
            var userManager = (UserManager<IdentityUser>?)scope
                                                            .ServiceProvider
                                                            .GetService(typeof(UserManager<IdentityUser>));
            if (userManager == null)
                throw new Exception($"Can not resolve {typeof(UserManager<IdentityUser>)}");


            // ------ ROLE MANAGER ------
            var roleManager = (RoleManager<IdentityRole>?)scope
                                                           .ServiceProvider
                                                           .GetService(typeof(RoleManager<IdentityRole>));

            if (roleManager == null)
                throw new Exception($"Can not resolve {typeof(RoleManager<IdentityRole>)}");

            seed(userManager, roleManager);
        }

        private void seedSuppliersAndOrders(IServiceScope scope)
        {
            // ------ APPLICATION DB CONTEXT ------
            var dbContext = (ApplicationDbContext?)scope
                                           .ServiceProvider
                                           .GetService(typeof(ApplicationDbContext));

            // -------------------- SUPPLIERS ---------------------------
            var suppliers = new List<Supplier>()
            {
                new Supplier(){
                        Id = Guid.NewGuid().ToString(),
                        BrandName = "Chairs Mate",
                        Product = new Product(){ 
                            Id = Guid.NewGuid().ToString(),
                            ProductName = "Chairs"
                        },
                        UnitPrice = 50,
                        Enabled = true,
                        PaymentTerms = 90,
                },new Supplier(){
                        Id = Guid.NewGuid().ToString(),
                        BrandName = "Lights ON",
                        Product = new Product(){
                            Id = Guid.NewGuid().ToString(),
                            ProductName = "Lamps"
                        },
                        UnitPrice = 50,
                        Enabled = true,
                        PaymentTerms = 60,
                },new Supplier(){
                        Id = Guid.NewGuid().ToString(),
                        BrandName = "Sleepy Inc.",
                        Product = new Product(){
                            Id = Guid.NewGuid().ToString(),
                            ProductName = "Mattresses",
                        },
                        UnitPrice = 300,
                        Enabled = true,
                        PaymentTerms = 30,
                },new Supplier(){
                        Id = Guid.NewGuid().ToString(),
                        BrandName = "Top Tisch",
                        Product = new Product(){
                            Id = Guid.NewGuid().ToString(),
                            ProductName = "Tables"
                        },
                        UnitPrice = 250,
                        Enabled = false,
                        PaymentTerms = 90,
                }};

            foreach (var supp in suppliers)
            {
                var exist = dbContext.Suppliers.Any(s => s.BrandName == supp.BrandName);
                if (!exist)
                    dbContext.Suppliers.AddRange(suppliers);
            }

            dbContext.SaveChanges();

            // -------------------- SUPPLIERS ---------------------------


        }
        private void seed(UserManager<IdentityUser> userManager,
                            RoleManager<IdentityRole> roleManager)
        {
            var defaultPassword = "asdasd";
            var raikoEmail = "raiko.vasilev@suchmasy.com";
            var mitkoEmail = "mitko.mitkov@suchmasy.com";
            var kiroEmail = "kiro.kirov@suchmasy.com";

            const string ROLE_ADMIN = "admin";
            const string ROLE_BUYER = "buyer";

            // =========================== USERS ===============================
            // try add user "raiko.vasilev"
            tryCreateUser(userManager, raikoEmail, defaultPassword);

            // try add user "mitko.mitkov"
            tryCreateUser(userManager, mitkoEmail, defaultPassword);

            // try add user "mitko.mitkov"
            tryCreateUser(userManager, kiroEmail, defaultPassword);


            // ============================ ROLES ==============================
            // try add role "admin"
            tryCreateRole(roleManager, "admin");

            // try add role "admin"
            tryCreateRole(roleManager, "buyer");


            // ============================ USER ROLES ==============================
            tryAddUserToRole(userManager, roleManager, raikoEmail, ROLE_ADMIN);
            tryAddUserToRole(userManager, roleManager, mitkoEmail, ROLE_BUYER);
        }

        private void tryAddUserToRole(UserManager<IdentityUser> userManager,
                                        RoleManager<IdentityRole> roleManager,
                                        string userEmail,
                                        string role)
        {

            var identityUser = userManager.FindByEmailAsync(userEmail).Result;
            if (identityUser == null)
            {
                throw new Exception($"Can not find user {userEmail}");
            }

            var res = userManager.AddToRoleAsync(identityUser, role).Result;

        }

        private void tryCreateUser(UserManager<IdentityUser> userManager, string email, string password)
        {
            if (userExists(userManager, email))
                return;

            IdentityUser user = new IdentityUser();
            user.UserName = email.Split('@').First();
            user.Email = email;
            user.EmailConfirmed = true;

            var res = userManager.CreateAsync(user, password).Result;
            if (!res.Succeeded)
                throw new Exception($"Can not create new user {user.UserName}");
        }

        private bool userExists(UserManager<IdentityUser> userManager, string userEmail)
        {
            var identityUser = userManager.FindByEmailAsync(userEmail).Result;
            if (identityUser == null)
                return false;
            return true;
        }

        private void tryCreateRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (roleExists(roleManager, roleName))
                return;

            IdentityRole identityRole = new IdentityRole(roleName);
            IdentityResult result = roleManager.CreateAsync(identityRole).Result;
            if (!result.Succeeded)
                throw new Exception($"Can not create role {roleName}");

        }

        private bool roleExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = roleManager.FindByNameAsync(roleName).Result;
            if (role == null)
                return false;
            return true;
        }

    }
}


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


