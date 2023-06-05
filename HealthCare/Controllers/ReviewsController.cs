using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthCare.Models;

namespace HealthCare.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ModelContext _context;

        public ReviewsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Reviews.Include(r => r.Web);
            return View(await modelContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Web)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,Rate,WebId")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", review.WebId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", review.WebId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ReviewId,Rate,WebId")] Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewId))
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
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", review.WebId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Web)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var review = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(decimal id)
        {
            return _context.Reviews.Any(e => e.ReviewId == id);
        }
    }
}
