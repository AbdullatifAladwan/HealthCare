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
    public class AppointmentsController : Controller
    {
        private readonly ModelContext _context;

        public AppointmentsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> ThankYoA()
        {

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.username = HttpContext.Session.GetString("Username");
            return View();



        }
        
            public async Task<IActionResult> DoYouD(decimal id)
        {
            ViewData["CardId"] = new SelectList(_context.Creditcards, "CardId", "Ccv");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname");

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.username = HttpContext.Session.GetString("Username");
            var result = await _context.Appointments
              .Include(d => d.Doctor)
              .Include(d => d.User)
              .FirstOrDefaultAsync(d => d.DoctorId == id);

            ViewBag.DoctorId = id;

            return View(result);
          



        }
        public async Task<IActionResult> DoYou()
        {
            ViewData["CardId"] = new SelectList(_context.Creditcards, "CardId", "Ccv");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname");

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.username = HttpContext.Session.GetString("Username");

            return View();


          
        }
        public async Task<IActionResult> AppoiDO(decimal id)
        {
            //ViewData["CardId"] = new SelectList(_context.Creditcards, "CardId", "Ccv");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.username = HttpContext.Session.GetString("Username");
           

            

            var result = await _context.Appointments
              .Include(d => d.Doctor)
              .Include(d => d.User)
              .FirstOrDefaultAsync(d => d.DoctorId == id);

             ViewBag.DoctorId =  id;

return View(result);
           

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AppoiDO([Bind("AppointmentId,Fullname,Data,Time,Phonenumber,Massage,AcceptOrReject,DoctorId,UserId,CardId")] Appointment appointment)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.username = HttpContext.Session.GetString("Username");

            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ThankYoA));
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
            ViewData["CardId"] = new SelectList(_context.Creditcards, "CardId", "Ccv", appointment.CardId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname", appointment.DoctorId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", appointment.UserId);
            return View(appointment);
        }
        public async Task<IActionResult> Appoi(decimal id)
        {
            ViewData["CardId"] = new SelectList(_context.Creditcards, "CardId", "Ccv");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname");

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.username = HttpContext.Session.GetString("Username");
           
            //var result = await _context.Creditcards
                
            //  .Include(d => d.CardId)
            //  .Include(d => d.UserId)
            //  .(d => d.CardId ==id)
            //  .FirstOrDefaultAsync(d => d.UserId == id);



            return View();


           
            //return View(result);
            //var modelContext = _context.Appointments.Include(a => a.Card).Include(a => a.Doctor).Include(a => a.User);
            //return View(await modelContext.ToListAsync());
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Appoi([Bind("AppointmentId,Fullname,Data,Time,Phonenumber,Massage,AcceptOrReject,DoctorId,UserId,CardId")] Appointment appointment)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.username = HttpContext.Session.GetString("Username");
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ThankYoA));
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
            ViewData["CardId"] = new SelectList(_context.Creditcards, "CardId", "Ccv", appointment.CardId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname", appointment.DoctorId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", appointment.UserId);
            return View(appointment);
        }
        public async Task<IActionResult> Edit2(decimal? id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["CardId"] = new SelectList(_context.Creditcards, "CardId", "Ccv", appointment.CardId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname", appointment.DoctorId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", appointment.UserId);
            return View(appointment);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit2(decimal id, [Bind("AppointmentId,Fullname,Data,Time,Phonenumber,Massage,AcceptOrReject,DoctorId,UserId,CardId")] Appointment appointment)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("AA", "Doctors");
            }
            ViewData["CardId"] = new SelectList(_context.Creditcards, "CardId", "Ccv", appointment.CardId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname", appointment.DoctorId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", appointment.UserId);
            return View(appointment);
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            var modelContext = _context.Appointments.Include(a => a.Card).Include(a => a.Doctor).Include(a => a.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Card)
                .Include(a => a.Doctor)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");

            ViewData["CardId"] = new SelectList(_context.Creditcards, "CardId", "Ccv");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,Fullname,Data,Time,Phonenumber,Massage,AcceptOrReject,DoctorId,UserId,CardId")] Appointment appointment)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.Creditcards, "CardId", "Ccv", appointment.CardId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname", appointment.DoctorId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", appointment.UserId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["CardId"] = new SelectList(_context.Creditcards, "CardId", "Ccv", appointment.CardId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname", appointment.DoctorId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", appointment.UserId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("AppointmentId,Fullname,Data,Time,Phonenumber,Massage,AcceptOrReject,DoctorId,UserId,CardId")] Appointment appointment)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["CardId"] = new SelectList(_context.Creditcards, "CardId", "Ccv", appointment.CardId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname", appointment.DoctorId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", appointment.UserId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Card)
                .Include(a => a.Doctor)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(decimal id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
