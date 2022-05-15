using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1_web_app;

namespace Lab1_web_app.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly DBBookingContext _context;

        public ReviewsController(DBBookingContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(int id, string name)
        {
            var dBBookingContext = _context.Reviews.Include(r => r.Accomodation).Include(r => r.User).Where(r => r.AccomodationId == id);

            ViewBag.AccomodationId = id;
            ViewBag.AccomodationName = name;

            return View(await dBBookingContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Accomodation)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create(int accomodationId, string accomodationName)
        {
            //ViewData["AccomodationId"] = new SelectList(_context.Accomodations, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");

            ViewBag.AccomodationId = accomodationId;
            ViewBag.AccomodatioName = accomodationName;

            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int accomodationId,[Bind("Id,UserId,AccomodationId,Rating,Comment")] Review review)
        {
            var accomodation = _context.Accomodations.Where(a => a.Id == accomodationId).FirstOrDefault();

            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Reviews", new { id = accomodation.Id, name = accomodation.Name });
            }

            //ViewData["AccomodationId"] = new SelectList(_context.Accomodations, "Id", "Id", review.AccomodationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", review.UserId);

            return RedirectToAction("Index", "Reviews", new { id = accomodation.Id, name = accomodation.Name });
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["AccomodationId"] = new SelectList(_context.Accomodations, "Id", "Id", review.AccomodationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", review.UserId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,AccomodationId,Rating,Comment")] Review review)
        {
            if (id != review.Id)
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
                    if (!ReviewExists(review.Id))
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
            ViewData["AccomodationId"] = new SelectList(_context.Accomodations, "Id", "Id", review.AccomodationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", review.UserId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Accomodation)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reviews == null)
            {
                return Problem("Entity set 'DBBookingContext.Reviews'  is null.");
            }
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
          return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
