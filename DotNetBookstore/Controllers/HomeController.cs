using DotNetBookstore.Data;
using DotNetBookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DotNetBookstore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Hardcoded list of featured book IDs
            var featuredBookIds = new List<int> { 2, 3, 4 };

            var featuredBooks = _context.Books
                .Include(b => b.Category)
                .Where(b => featuredBookIds.Contains(b.BookId))
                .OrderBy(b => b.Author)
                .ThenBy(b => b.Title);
            return View(await featuredBooks.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
