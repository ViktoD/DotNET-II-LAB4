using Lab4.Data;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    public class BookController : Controller
    {
        private readonly DbConnection _db;

        public BookController(DbConnection db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Book> booksList = _db.Books;
            return View(booksList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {

            if (ModelState.IsValid)
            {
                await _db.Books.AddAsync(book);
                await _db.SaveChangesAsync();
                TempData["success"] = "Book was added successfully";
                return RedirectToAction("Index");
            }

            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var bookFromDb = await _db.Books.FindAsync(id);

            if (bookFromDb == null)
            {
                return NotFound();
            }

            return View(bookFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Update(book);
                await _db.SaveChangesAsync();
                TempData["success"] = "Book was updated successfully";
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var bookFromDb = await _db.Books.FindAsync(id);

            if (bookFromDb == null)
            {
                return NotFound();
            }

            return View(bookFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var bookFromDb = await _db.Books.FindAsync(id);

            if (bookFromDb == null)
            {
                return NotFound();
            }
            _db.Books.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Book was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
