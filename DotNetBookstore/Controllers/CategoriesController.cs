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
            // if category empty,redirect to index so user can first select a category
            if (string.IsNullOrEmpty(category))
            {
                return RedirectToAction("Index");
            }
            ViewBag.Category = category;
            //ViewData["Category"] = category;
            return View();
        }
    }
}
