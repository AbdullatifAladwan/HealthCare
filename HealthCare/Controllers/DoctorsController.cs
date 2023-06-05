using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthCare.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HealthCare.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public DoctorsController(ModelContext context, IWebHostEnvironment WebHostEnvironment)
        {
            _context = context;
            _webHostEnviroment = WebHostEnvironment;


        }

        // GET: Doctors
        public async Task<IActionResult> AA(string id ,decimal id1)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            //var modelContext = _context.Appointments.Include(d => d.User).Include(d => d.Doctor);
            //return View(await modelContext.ToListAsync());
            var doctor = await _context.Appointments
               .Include(d => d.Doctor)
               .Include(d => d.User)
               .FirstOrDefaultAsync(d => d.AppointmentId == id1);

            ViewBag.AppointmentId = id1;


            var movies = from m in _context.Appointments.Include(d => d.Doctor).Include(d => d.User)
                         select m;
              if (id != null)
            {
                var SearchResult = await movies.Where(x => x.Doctor.Firstname == id).ToListAsync();
                return View(SearchResult);
            }
            return View(await movies.ToListAsync());
        }
        public async Task<IActionResult> DoctorDet(decimal? id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            var doctor = await _context.Doctors
               .Include(d => d.Role)
               .Include(d => d.Specialization)
               .FirstOrDefaultAsync(m => m.DoctorId == id);
            return View(doctor);
            var modelContext = _context.Doctors.Include(d => d.Role).Include(d => d.Specialization);
            return View(await modelContext.ToListAsync());
        }
        public async Task<IActionResult> DoctorIndex()
        {
            ViewBag.username = HttpContext.Session.GetString("Username");
            var modelContext = _context.Doctors.Include(d => d.Role).Include(d => d.Specialization);
            return View(await modelContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> DoctorIndex( string searchString)
        {
            var movies = from m in _context.Doctors.Include(d => d.Role).Include(d => d.Specialization)
                         select m;
            ViewBag.username = HttpContext.Session.GetString("Username");
            var modelContext = _context.Doctors.Include(d => d.Role).Include(d => d.Specialization);
          
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Firstname!.Contains(searchString));
                return View(await movies.ToListAsync());

            }
            return View(await modelContext.ToListAsync());

        }
    
       
        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            var modelContext = _context.Doctors.Include(d => d.Role).Include(d => d.Specialization);
            return View(await modelContext.ToListAsync());
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .Include(d => d.Role)
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "Specialization1");
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorId,Username,Firstname,Lastname,Imagepath,ImageFile,Password,Phonenumber,Address,Email,RoleId,SpecializationId")] Doctor doctor)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (ModelState.IsValid)
            {
                if (doctor.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string filename = Guid.NewGuid().ToString() + "_" + doctor.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/Images/" + filename);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await doctor.ImageFile.CopyToAsync(filestream);


                    }
                    doctor.Imagepath = filename;
                }
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", doctor.RoleId);
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "Specialization1", doctor.SpecializationId);
            return View(doctor);
        }

        // GET: Doctors/Edit/5
      
        public async Task<IActionResult> Edit1(decimal? id)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", doctor.RoleId);
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "Specialization1", doctor.SpecializationId);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit1(decimal id, [Bind("DoctorId,Username,Firstname,Lastname,Imagepath,ImageFile,Password,Phonenumber,Address,Email,RoleId,SpecializationId")] Doctor doctor)
        {
            ViewBag.username = HttpContext.Session.GetString("Username1");
            ViewBag.DoctorId = HttpContext.Session.GetInt32("DoctorId");
            if (id != doctor.DoctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (doctor.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + doctor.ImageFile.FileName;
                        string extension = Path.GetExtension(doctor.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await doctor.ImageFile.CopyToAsync(fileStream);
                        }
                        doctor.Imagepath = fileName;
                    }

                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.DoctorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("HomeDoctor", "Home");
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", doctor.RoleId);
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "Specialization1", doctor.SpecializationId);
            return View(doctor);
        }

        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", doctor.RoleId);
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "Specialization1", doctor.SpecializationId);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("DoctorId,Username,Firstname,Lastname,Imagepath,ImageFile,Password,Phonenumber,Address,Email,RoleId,SpecializationId")] Doctor doctor)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id != doctor.DoctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (doctor.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + doctor.ImageFile.FileName;
                        string extension = Path.GetExtension(doctor.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await doctor.ImageFile.CopyToAsync(fileStream);
                        }
                        doctor.Imagepath = fileName;
                    }

                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.DoctorId))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", doctor.RoleId);
            ViewData["SpecializationId"] = new SelectList(_context.Specializations, "SpecializationId", "Specialization1", doctor.SpecializationId);
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .Include(d => d.Role)
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(decimal id)
        {
            return _context.Doctors.Any(e => e.DoctorId == id);
        }
    }
}
