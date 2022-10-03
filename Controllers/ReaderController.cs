using Lab4.Data;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    public class ReaderController : Controller
    {
        private readonly DbConnection _db;

        public ReaderController(DbConnection db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Reader> readerList = _db.Readers;
            return View(readerList);
        }

        
        public IActionResult Create()
        {   
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reader reader)
        {
            if (ModelState.IsValid)
            {
                await _db.Readers.AddAsync(reader);
                await _db.SaveChangesAsync();
                TempData["success"] = "Reader was added successfully";
                return RedirectToAction("Index");
            }
            return View(reader);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null || id <= 0)
            {
                return NotFound();
            }

            var readerFromDb = await _db.Readers.FindAsync(id);

            if(readerFromDb == null)
            {
                return NotFound();
            }

            return View(readerFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Reader reader)
        {
            if (ModelState.IsValid)
            {
                _db.Readers.Update(reader);
                await _db.SaveChangesAsync();
                TempData["success"] = "Reader was updated successfully";
                return RedirectToAction("Index");
            }
            return View(reader);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var readerFromDb = await _db.Readers.FindAsync(id);

            if (readerFromDb == null)
            {
                return NotFound();
            }

            return View(readerFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var readerFromDb = await _db.Readers.FindAsync(id);

            if (readerFromDb == null)
            {
                return NotFound();
            }
            _db.Readers.Remove(readerFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Reader was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
