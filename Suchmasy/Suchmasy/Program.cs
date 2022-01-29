using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Suchmasy;
using Suchmasy.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ContainerConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
}).AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

const string SUPPLIER_RELATIONSHIP = "supplier_relationship";

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(SUPPLIER_RELATIONSHIP, policy =>
                                        policy.RequireAuthenticatedUser()
                                              .RequireRole("admin", "buyer"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Suppliers", SUPPLIER_RELATIONSHIP);
    //options.Conventions.AuthorizePage("/Contact");
    //options.Conventions.AllowAnonymousToPage("/Private/PublicPage");
    //options.Conventions.AllowAnonymousToFolder("/Private/PublicPages");
});

var app = builder.Build();

// SEED initial data
new InitialData(app.Services).Seed();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
