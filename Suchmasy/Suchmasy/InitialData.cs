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
                seedSuppliersProductsAndRequests(scope);
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

        private void seedSuppliersProductsAndRequests(IServiceScope scope)
        {
            // ------ APPLICATION DB CONTEXT ------
            var dbContext = (ApplicationDbContext?)scope
                                           .ServiceProvider
                                           .GetService(typeof(ApplicationDbContext));

            // -------------------- PRODUCTS ---------------------------
            var chairsProd = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                ProductName = "Chairs"
            };

            var lampsProd = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                ProductName = "Lamps"
            };

            var mattressesProd = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                ProductName = "Mattresses",
            };

            var tablesProd = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                ProductName = "Tables"
            };

            var products = new List<Product>() { chairsProd, lampsProd, mattressesProd, tablesProd };

            foreach (var prod in products)
            {
                var exist = dbContext.Products.Any(p => p.ProductName == prod.ProductName);
                if (!exist)
                    dbContext.Products.AddRange(products);
            }

            // -------------------- SUPPLIERS ---------------------------
            var suppliers = new List<Supplier>()
            {
                new Supplier(){
                        Id = Guid.NewGuid().ToString(),
                        BrandName = "Chairs Mate",
                        Product = chairsProd,
                        UnitPrice = 50,
                        Enabled = true,
                        PaymentTerms = 90,
                },new Supplier(){
                        Id = Guid.NewGuid().ToString(),
                        BrandName = "Lights ON",
                        Product = lampsProd,
                        UnitPrice = 50,
                        Enabled = true,
                        PaymentTerms = 60,
                },new Supplier(){
                        Id = Guid.NewGuid().ToString(),
                        BrandName = "Sleepy Inc.",
                        Product = mattressesProd,
                        UnitPrice = 300,
                        Enabled = true,
                        PaymentTerms = 30,
                },new Supplier(){
                        Id = Guid.NewGuid().ToString(),
                        BrandName = "Top Tisch",
                        Product = tablesProd,
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


            // -------------------- REQUESTS ---------------------------

            var anyRequests = dbContext.Requests.Any();
            if (!anyRequests)
            {
                var requesterKiro = dbContext.Users.First(u => u.Email == "kiro.kirov@suchmasy.com");

                var requests = new List<Request>()
                            {
                                new Request(){
                                    Id = Guid.NewGuid().ToString(),
                                    Product = "Cozy chairs",
                                    Quantity = 20,
                                    RequesterId = requesterKiro.Id,
                                    RequesterEmail = requesterKiro.Email,
                                    PlacedOn = new DateTime(2021, 1, 5, 10, 11,21),
                                    Status = RequestStatus.Cancelled,
                                    ClosedByEmail = requesterKiro.Email,
                                    ClosedOn = DateTime.Now,
                                    Text = "This product is needed for confidential reasons!This product is needed for confidential reasons!This product is needed for confidential reasons!"
                                },
                                new Request(){
                                    Id = Guid.NewGuid().ToString(),
                                    Product = "Big tables",
                                    Quantity = 5,
                                    RequesterId = requesterKiro.Id,
                                    RequesterEmail = requesterKiro.Email,
                                    PlacedOn = new DateTime(2021, 5, 6, 15, 43,12),
                                    Status = RequestStatus.Submitted,
                                    //ClosedByEmail = requesterKiro.Email,
                                    //ClosedOn = DateTime.Now,
                                    Text = "This product is needed for confidential reasons!"
                                },
                                new Request(){
                                    Id = Guid.NewGuid().ToString(),
                                    Product = "Big tables",
                                    Quantity = 5,
                                    RequesterId = requesterKiro.Id,
                                    RequesterEmail = requesterKiro.Email,
                                    PlacedOn = DateTime.Now,
                                    Status = RequestStatus.Submitted,
                                    Text = "This product is needed for confidential reasons!"
                                },
                            };

                dbContext.Requests.AddRange(requests);
            }

            // KEEP AT THE END:
            dbContext.SaveChanges();
        }
        private void seed(UserManager<IdentityUser> userManager,
                            RoleManager<IdentityRole> roleManager)
        {
            var defaultPassword = "asdasd";
            var raikoEmail = "raiko.vasilev@suchmasy.com";
            var mitkoEmail = "mitko.mitkov@suchmasy.com";
            var kiroEmail = "kiro.kirov@suchmasy.com";
            var metoEmail = "meto.metov@suchmasy.com";
            var requesterBuyerEmail = "requester.buyer@suchmasy.com";

            const string ROLE_ADMIN = "admin";
            const string ROLE_BUYER = "buyer";
            const string ROLE_REQUESTER = "requester";

            // =========================== USERS ===============================
            // try add user "raiko.vasilev"
            tryCreateUser(userManager, raikoEmail, defaultPassword);

            // try add user "mitko.mitkov"
            tryCreateUser(userManager, mitkoEmail, defaultPassword);

            // try add user "kiro.kirov@suchmasy.com"
            tryCreateUser(userManager, kiroEmail, defaultPassword);

            // try add user "meto.metov@suchmasy.com"
            tryCreateUser(userManager, metoEmail, defaultPassword);


            // ============================ ROLES ==============================
            // try add role "admin"
            tryCreateRole(roleManager, ROLE_ADMIN);

            // try add role "buyer"
            tryCreateRole(roleManager, ROLE_BUYER);

            // try add role "requester"
            tryCreateRole(roleManager, ROLE_REQUESTER);


            // ============================ USER ROLES ==============================
            tryAddUserToRole(userManager, roleManager, raikoEmail, ROLE_ADMIN);
            tryAddUserToRole(userManager, roleManager, mitkoEmail, ROLE_BUYER);
            tryAddUserToRole(userManager, roleManager, kiroEmail, ROLE_REQUESTER);
            tryAddUserToRole(userManager, roleManager, metoEmail, ROLE_REQUESTER);
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

            // TODO: remove 
            var claimAdded = userManager.AddClaimAsync(user, new Claim("PasswordChanged", "")).Result;
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


