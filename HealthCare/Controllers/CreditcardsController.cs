using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthCare.Models;
using Microsoft.AspNetCore.Http;

namespace HealthCare.Controllers
{
    public class CreditcardsController : Controller
    {
        private readonly ModelContext _context;

        public CreditcardsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Creditcards

        public async Task<IActionResult> Card(decimal? id)
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.username = HttpContext.Session.GetString("Username");
            var result = await _context.Creditcards
             
             .Include(d => d.User)
             .FirstOrDefaultAsync(d => d.UserId == id);
            ViewBag.UserId = id;

            return View(result);
            var modelContext = _context.Creditcards.Include(c => c.User);
            return View(await modelContext.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Card([Bind("CardId,CardNumber,Ccv,Balance,ExpireData,UserId")] Creditcard creditcard)
        {

            if (ModelState.IsValid)
            {
                _context.Add(creditcard);
                await _context.SaveChangesAsync();
                return  RedirectToAction("Appoi", "Appointments");
            }
            //Appointment appointment1 = new Appointment
            //{
            //    Fullname = appointment.Fullname,
            //    DoctorId= appointment.,
            //    Data  = appointment.Data,
            //    Time=appointment.Time  ,
            //    Phonenumber=appointment.Phonenumber,
            //    Massage=appointment.Massage,
            //    CardId = appointment.CardId,
            //    UserId=  appointment.UserId,
            //};
            //_context.Add(appointment1);
            //await _context.SaveChangesAsync();
          
           
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", creditcard.UserId);
            return View(creditcard);
        }
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Creditcards.Include(c => c.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Creditcards/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditcard = await _context.Creditcards
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (creditcard == null)
            {
                return NotFound();
            }

            return View(creditcard);
        }

        // GET: Creditcards/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname");
            return View();
        }

        // POST: Creditcards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardId,CardNumber,Ccv,Balance,ExpireData,UserId")] Creditcard creditcard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creditcard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", creditcard.UserId);
            return View(creditcard);
        }

        // GET: Creditcards/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditcard = await _context.Creditcards.FindAsync(id);
            if (creditcard == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", creditcard.UserId);
            return View(creditcard);
        }

        // POST: Creditcards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("CardId,CardNumber,Ccv,Balance,ExpireData,UserId")] Creditcard creditcard)
        {
            if (id != creditcard.CardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditcard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditcardExists(creditcard.CardId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", creditcard.UserId);
            return View(creditcard);
        }

        // GET: Creditcards/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditcard = await _context.Creditcards
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (creditcard == null)
            {
                return NotFound();
            }

            return View(creditcard);
        }

        // POST: Creditcards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var creditcard = await _context.Creditcards.FindAsync(id);
            _context.Creditcards.Remove(creditcard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditcardExists(decimal id)
        {
            return _context.Creditcards.Any(e => e.CardId == id);
        }
    }
}
