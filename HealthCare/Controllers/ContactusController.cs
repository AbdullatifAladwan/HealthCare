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
    public class ContactusController : Controller
    {
        private readonly ModelContext _context;

        public ContactusController(ModelContext context)
        {
            _context = context;
        }

        // GET: Contactus
        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            var modelContext = _context.Contactus.Include(c => c.Web);
            return View(await modelContext.ToListAsync());
        }

        // GET: Contactus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var contactu = await _context.Contactus
                .Include(c => c.Web)
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contactu == null)
            {
                return NotFound();
            }

            return View(contactu);
        }
        public async Task<IActionResult> ThankYou()
        {
            ViewBag.username = HttpContext.Session.GetString("Username");
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId");
            return View();
        }
        public async Task<IActionResult> Contact()
        {
            ViewBag.username = HttpContext.Session.GetString("Username");
            
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId");
            return View();
        }
        // GET: Contactus/Create
        public IActionResult Create()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([Bind("ContactId,Firstname,Lastname,Phonenummber,Email,Description,WebId")] Contactu contactu)
        {
            ViewBag.username = HttpContext.Session.GetString("Username");

            if (ModelState.IsValid)
            {
                _context.Add(contactu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ThankYou));
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", contactu.WebId);
            return View(contactu);
        }
        // POST: Contactus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,Firstname,Lastname,Phonenummber,Email,Description,WebId")] Contactu contactu)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (ModelState.IsValid)
            {
                _context.Add(contactu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", contactu.WebId);
            return View(contactu);
        }

        // GET: Contactus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var contactu = await _context.Contactus.FindAsync(id);
            if (contactu == null)
            {
                return NotFound();
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", contactu.WebId);
            return View(contactu);
        }

        // POST: Contactus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ContactId,Firstname,Lastname,Phonenummber,Email,Description,WebId")] Contactu contactu)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id != contactu.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactuExists(contactu.ContactId))
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
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", contactu.WebId);
            return View(contactu);
        }

        // GET: Contactus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var contactu = await _context.Contactus
                .Include(c => c.Web)
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contactu == null)
            {
                return NotFound();
            }

            return View(contactu);
        }

        // POST: Contactus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var contactu = await _context.Contactus.FindAsync(id);
            _context.Contactus.Remove(contactu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactuExists(decimal id)
        {
            return _context.Contactus.Any(e => e.ContactId == id);
        }
    }
}
