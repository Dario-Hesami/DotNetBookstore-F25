using DotNetBookstore.Data;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace DotNetBookstore.Controllers
{
    public class ShopController : Controller
    {
        // Class-level DbContext connection object
        private readonly ApplicationDbContext _context;
        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.OrderBy(c => c.Name).ToList();
            return View(categories);
        }
    }
}
