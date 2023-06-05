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
    public class AttendancesController : Controller
    {
        private readonly ModelContext _context;

        public AttendancesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> AA(String id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            //var modelContext = _context.Appointments.Include(d => d.User).Include(d => d.Doctor);
            //return View(await modelContext.ToListAsync());
            //var doctor = await _context.Appointments
            //   .Include(d => d.Doctor)
            //   .Include(d => d.User)


            var movies = from m in _context.Attendances.Include(d => d.Doctor)
                         select m;
            if (id != null)
            {
                var SearchResult = await movies.Where(x => x.Doctor.Firstname == id).ToListAsync();
                return View(SearchResult);
            }
            return View(await movies.ToListAsync());
        }
            public async Task<IActionResult> Index()
        {
            var modelContext = _context.Attendances.Include(a => a.Doctor);
            return View(await modelContext.ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public async Task<IActionResult> Create(decimal id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname");
            var doctor = await _context.Attendances
               
               .Include(d => d.Doctor)
               .FirstOrDefaultAsync(m => m.DoctorId == id);
            ViewBag.DoctorId = id;
            return View(doctor);

        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendanceId,Time,Day,DoctorName,DoctorId")] Attendance attendance)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            //var modelContext = _context.Attendances.Include(d => d.Doctor).FirstOrDefaultAsync(m => m.DoctorId == id); 
            //ViewBag.DoctorId = id;
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction("AA", "Attendances");
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname", attendance.DoctorId);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname", attendance.DoctorId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("AttendanceId,Time,Day,DoctorName,DoctorId")] Attendance attendance)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (id != attendance.AttendanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttendanceId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname", attendance.DoctorId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(decimal id)
        {
            return _context.Attendances.Any(e => e.AttendanceId == id);
        }
    }
}
