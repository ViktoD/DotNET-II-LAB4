using Lab4.Data;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Controllers
{
    public class InfoBookController : Controller
    {

        private readonly DbConnection _db;

        public InfoBookController(DbConnection db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            IEnumerable<InfoBook> infoBooks = _db.InfoBooks.Include(prop => prop.Ticket).Include(prop => prop.Book).Include(prop => prop.Ticket.Reader).OrderBy(prop=>prop.Ticket.Reader.Surname);
            return View(infoBooks);
        }


        public IActionResult Create()
        {
            ViewBag.Books = new SelectList(_db.Books, "ID", "Name");
            ViewBag.Tickets = new SelectList(_db.Tickets, "ID", "ID");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InfoBook infoBook)
        {      
            if (infoBook.BookID != 0 && infoBook.TicketID != 0 && infoBook.DateTakeBook != new DateTime(0001,1,1))
            {
                await _db.InfoBooks.AddAsync(infoBook);
                await _db.SaveChangesAsync();
                TempData["success"] = "New information about book was added successfully";
                return RedirectToAction("Index");
            }
            
            ViewBag.Books = new SelectList(_db.Books, "ID", "Name");
            ViewBag.Tickets = new SelectList(_db.Tickets, "ID", "ID");

            return View(infoBook);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var infoBookFromDb = _db.InfoBooks.Include(p => p.Ticket).Include(p => p.Ticket.Reader).Include(p => p.Book).First(p => p.ID == id);

            if (infoBookFromDb == null)
            {
                return NotFound();
            }

            return View(infoBookFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var infoBookFromDb = await _db.InfoBooks.FindAsync(id);

            if (infoBookFromDb == null)
            {
                return NotFound();
            }
            _db.InfoBooks.Remove(infoBookFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Information about book was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
