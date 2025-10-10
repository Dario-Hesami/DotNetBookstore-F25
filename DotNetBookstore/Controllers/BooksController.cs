using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetBookstore.Data;
using DotNetBookstore.Models;

namespace DotNetBookstore.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Books.Include(b => b.Category).OrderBy(b => b.Author).ThenBy(b => b.Title);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "CategoryId", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Use Bind to include only specific Book properties from form data
        // The Image property is excluded from model binding because the uploaded file is processed separately via the IFormFile parameter
        // Image property in the Book model is a string to store the file name of the uploaded image - it is nullable to allow creating a book without an image - IFormFile parameter to receive the uploaded image file
        public async Task<IActionResult> Create([Bind("BookId,Author,Title,Price,MatureContent,CategoryId")] Book book, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                // handle image upload
                if (Image != null && Image.Length > 0)
                {
                    // call the UploadImage method to upload the image and get the file name
                    var fileName = UploadImage(Image);
                    // set the Image property of the book to the file name
                    book.Image = fileName;
                }
                else
                {
                    // if no image is uploaded, set the Image property to null or a default image
                    book.Image = null; // or "default.jpg" if you have a default image
                }
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", book.CategoryId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "CategoryId", "Name", book.CategoryId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Author,Title,Price,MatureContent,CategoryId")] Book book, IFormFile? Image, string? CurrentImage)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // upload image if there is one
                    if (Image != null && Image.Length > 0)
                    {
                        book.Image = UploadImage(Image);
                    }
                    else
                    {
                        // keep the current image if no new image is uploaded
                        // if this book already has an image
                        if (CurrentImage != null)
                        {
                            book.Image = CurrentImage;
                        }
                    }
                    _context.Update(book);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", book.CategoryId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }

        // method to upload image to wwwroot/images/books folder
        private static string UploadImage(IFormFile image)
        {
            // check if image is null
            if (image == null || image.Length == 0)
            {
                return string.Empty;
            }
            // set destination path dynamically so it runs on any system
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books");
            // ensure the images/books folder exists
            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }
            // use globally unique identifier (GUID) to create a unique file name
            // e.g., 12345678-1234-1234-1234-123456789012-book1.jpg
            var fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
            // combine the imagesPath and file name
            var uploadPath = Path.Combine(imagesPath, fileName);
            // create the file and copy the image content to it
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            // return new file name
            return fileName;
        }
    }
}
