using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthCare.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace HealthCare.Controllers
{
    public class AboutusController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public AboutusController(ModelContext context, IWebHostEnvironment WebHostEnvironment)
        {
            _context = context;
            _webHostEnviroment = WebHostEnvironment;
        }
        // GET: Aboutus
        public async Task<IActionResult> Servis()
        {
            ViewBag.username = HttpContext.Session.GetString("Username");
           
            var modelContext = _context.Aboutus.Include(a => a.Web);
            return View(await modelContext.ToListAsync());
            
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Username");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");

            var modelContext = _context.Aboutus.Include(a => a.Web);
            return View(await modelContext.ToListAsync());
        }
        public async Task<IActionResult> About()
        {
           
            var item1 = _context.Testimonials.ToList();
            var item2 = _context.Aboutus.ToList();
            var item3 = _context.Doctors.Include(d => d.Role).Include(d => d.Specialization).ToList();
            ViewBag.username = HttpContext.Session.GetString("Username");

            var collection = new Tuple<IEnumerable<HealthCare.Models.Testimonial>, IEnumerable<HealthCare.Models.Aboutu>, IEnumerable<HealthCare.Models.Doctor>>(item1, item2,item3);


            
            return View(collection);
        }

        // GET: Aboutus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Username");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus
                .Include(a => a.Web)
                .FirstOrDefaultAsync(m => m.AboutusId == id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // GET: Aboutus/Create
        public IActionResult Create()

        {
            ViewBag.AdminName = HttpContext.Session.GetString("Username");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId");
            return View();
        }

        // POST: Aboutus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AboutusId,BackgroundImage,ImageFile,Description,Pargraph1,Pargraph2,Pargraph3,WebId")] Aboutu aboutu)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Username");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (ModelState.IsValid)
            {
                if (aboutu.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string filename = Guid.NewGuid().ToString() + "_" + aboutu.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/Images/" + filename);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await aboutu.ImageFile.CopyToAsync(filestream);


                    }
                    aboutu.BackgroundImage = filename;
                }

                _context.Add(aboutu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(aboutu);
        }

        // GET: Aboutus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Username");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus.FindAsync(id);
            if (aboutu == null)
            {
                return NotFound();
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", aboutu.WebId);
            return View(aboutu);
        }

        // POST: Aboutus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("AboutusId,BackgroundImage,ImageFile,Description,Pargraph1,Pargraph2,Pargraph3,WebId")] Aboutu aboutu)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Username");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id != aboutu.AboutusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutu.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + aboutu.ImageFile.FileName;
                        string extension = Path.GetExtension(aboutu.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await aboutu.ImageFile.CopyToAsync(fileStream);
                        }
                        aboutu.BackgroundImage = fileName;
                    }
                    _context.Update(aboutu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutuExists(aboutu.AboutusId))
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
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", aboutu.WebId);
            return View(aboutu);
        }

        // GET: Aboutus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("Username");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus
                .Include(a => a.Web)
                .FirstOrDefaultAsync(m => m.AboutusId == id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // POST: Aboutus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var aboutu = await _context.Aboutus.FindAsync(id);
            _context.Aboutus.Remove(aboutu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutuExists(decimal id)
        {
            return _context.Aboutus.Any(e => e.AboutusId == id);
        }
    }
}
