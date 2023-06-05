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
    public class UsersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public UsersController(ModelContext context, IWebHostEnvironment WebHostEnvironment)
        {
            _context = context;
            _webHostEnviroment = WebHostEnvironment;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            var modelContext = _context.Users.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Firstname,Lastname,Imagepath,ImageFile,Password,Phonenumber,Address,Email,RoleId")] User user)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (ModelState.IsValid)
            {
                if (user.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string filename = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/Images/" + filename);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(filestream);


                    }
                    user.Imagepath = filename;
                }
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("UserId,Username,Firstname,Lastname,Imagepath,ImageFile,Password,Phonenumber,Address,Email,RoleId")] User user)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (user.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                        string extension = Path.GetExtension(user.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await user.ImageFile.CopyToAsync(fileStream);
                        }
                        user.Imagepath = fileName;
                    }

                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }
        public async Task<IActionResult> Edit1(decimal? id)
        {

            ViewBag.username = HttpContext.Session.GetString("Username");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit1(decimal id, [Bind("UserId,Username,Firstname,Lastname,Imagepath,ImageFile,Password,Phonenumber,Address,Email,RoleId")] User user)
        {
            ViewBag.username = HttpContext.Session.GetString("Username");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (user.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                        string extension = Path.GetExtension(user.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await user.ImageFile.CopyToAsync(fileStream);
                        }
                        user.Imagepath = fileName;
                    }


                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("HomeUser", "Home");
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(decimal id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
