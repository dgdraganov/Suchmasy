using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Suchmasy.Data;
using Suchmasy.Models;

namespace Suchmasy.Pages
{
    public class Suppliers : PageModel
    {
        //public Suppliers(ApplicationDbContext dbContext)
        //{
        //    //_dbContext = dbContext;
        //}
       // private ApplicationDbContext _dbContext { get; }
        //public IEnumerable<Supplier> SupplierList { get; set; }

        public void OnGet()
        {
            //SupplierList = _dbContext.Suppliers.OrderBy(s => s.BrandName);
        }
    }
}
