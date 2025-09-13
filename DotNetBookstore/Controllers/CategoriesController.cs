using Microsoft.AspNetCore.Mvc;

namespace DotNetBookstore.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Browse(string category)
        {
            ViewBag.Category = category;
            //ViewData["Category"] = category;
            return View();
        }
    }
}
