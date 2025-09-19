using DotNetBookstore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBookstore.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            // Use the Category model to generate 10 sample categories in memory for displaying in the view
            var categories = new List<Category>();
            for (int i = 1; i <= 10; i++)
            {
                categories.Add(new Category
                {
                    CategoryId = i,
                    Name = $"Category {i}" // = "Category " + i
                });
            }
            return View(categories);
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

        public IActionResult Create() 
        {
            return View();
        }
    }
}
