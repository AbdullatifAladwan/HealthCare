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
    public class WebsitesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public WebsitesController(ModelContext context, IWebHostEnvironment WebHostEnvironment)
        {
            _context = context;
            _webHostEnviroment = WebHostEnvironment;
        }

        // GET: Websites
        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            var modelContext = _context.Websites.Include(w => w.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Websites/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Websites
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.WebId == id);
            if (website == null)
            {
                return NotFound();
            }

            return View(website);
        }

        // GET: Websites/Create
        public IActionResult Create()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname");
            return View();
        }

        // POST: Websites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WebId,BackgroundImage,LogoPic,SlidrPic,ImageFile,Pargraph,UserId")] Website website)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (ModelState.IsValid)
            {
                if (website.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string filename = Guid.NewGuid().ToString() + "_" + website.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/Images/" + filename);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await website.ImageFile.CopyToAsync(filestream);


                    }
                    website.SlidrPic = filename;
                }

                _context.Add(website);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", website.UserId);
            return View(website);
        }

        // GET: Websites/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Websites.FindAsync(id);
            if (website == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", website.UserId);
            return View(website);
        }

        // POST: Websites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("WebId,BackgroundImage,LogoPic,SlidrPic,ImageFile,Pargraph,UserId")] Website website)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id != website.WebId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (website.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + website.ImageFile.FileName;
                        string extension = Path.GetExtension(website.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await website.ImageFile.CopyToAsync(fileStream);
                        }
                        website.SlidrPic = fileName;
                    }
                    _context.Update(website);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebsiteExists(website.WebId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Firstname", website.UserId);
            return View(website);
        }

        // GET: Websites/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
           
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Websites
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.WebId == id);
            if (website == null)
            {
                return NotFound();
            }

            return View(website);
        }

        // POST: Websites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var website = await _context.Websites.FindAsync(id);
            _context.Websites.Remove(website);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebsiteExists(decimal id)
        {
            return _context.Websites.Any(e => e.WebId == id);
        }
    }
}
