using Lab4.Data;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Controllers
{
    public class TicketController : Controller
    {
        private readonly DbConnection _db;

        public TicketController(DbConnection db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Ticket> ticketsList = _db.Tickets.Include(prop => prop.Reader);
            return View(ticketsList);
        }

        public IActionResult Create()
        {
            ViewBag.Readers = new SelectList(_db.Readers, "ID", "Surname");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ticket.ReaderID != 0 && ticket.DateStart != new DateTime(0001,1,1) && ticket.DateEnd != new DateTime(0001,1,1))
            {
                await _db.Tickets.AddAsync(ticket);
                await _db.SaveChangesAsync();
                TempData["success"] = "Ticket was created successfully";
                return RedirectToAction("Index");
            }
            ViewBag.Readers = new SelectList(_db.Readers, "ID", "Surname");
            return View(ticket);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

           var ticketFromDb = await _db.Tickets.FindAsync(id);
            if (ticketFromDb == null)
            {
                return NotFound();
            }

            return View(ticketFromDb);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var ticketFromDb = await _db.Tickets.FindAsync(id);

            if (ticketFromDb == null)
            {
                return NotFound();
            }

            return View(ticketFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var ticketFromDb = await _db.Tickets.FindAsync(id);

            if (ticketFromDb == null)
            {
                return NotFound();
            }
            _db.Tickets.Remove(ticketFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Information about ticket was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
