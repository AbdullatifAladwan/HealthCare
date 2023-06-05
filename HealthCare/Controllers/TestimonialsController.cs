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
    public class TestimonialsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;


        public TestimonialsController(ModelContext context, IWebHostEnvironment WebHostEnvironment)
        {
            _context = context;
            _webHostEnviroment = WebHostEnvironment;

        }

        // GET: Testimonials
        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            var modelContext = _context.Testimonials.Include(t => t.Web);
            return View(await modelContext.ToListAsync());
        }

        // GET: Testimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.Web)
                .FirstOrDefaultAsync(m => m.TestId == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // GET: Testimonials/Create
        public IActionResult Create()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId");
            return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestId,Name,Imagepath,ImageFile,Feedback,WebId")] Testimonial testimonial)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (ModelState.IsValid)
            {
                if (testimonial.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string filename = Guid.NewGuid().ToString() + "_" + testimonial.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/Images/" + filename);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await testimonial.ImageFile.CopyToAsync(filestream);


                    }
                    testimonial.Imagepath = filename;
                }

                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", testimonial.WebId);
            return View(testimonial);
        }

        // GET: Testimonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", testimonial.WebId);
            return View(testimonial);
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("TestId,Name,Imagepath,ImageFile,Feedback,WebId")] Testimonial testimonial)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id != testimonial.TestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (testimonial.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + testimonial.ImageFile.FileName;
                        string extension = Path.GetExtension(testimonial.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await testimonial.ImageFile.CopyToAsync(fileStream);
                        }
                        testimonial.Imagepath = fileName;
                    }
                    _context.Update(testimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(testimonial.TestId))
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
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", testimonial.WebId);
            return View(testimonial);
        }

        // GET: Testimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.Web)
                .FirstOrDefaultAsync(m => m.TestId == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            var testimonial = await _context.Testimonials.FindAsync(id);
            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialExists(decimal id)
        {
            return _context.Testimonials.Any(e => e.TestId == id);
        }
    }
}

