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

            var accomodation = _context.Accomodations.Where(a => a.Id == id).Include(a => a.Type).First();

            ViewBag.AccomodationId = id;
            ViewBag.AccomodationName = name;
            ViewBag.AccomodationTypeId = accomodation.TypeId;
            ViewBag.AccomodationTypeName = accomodation.Type.Name;

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
            var accomodation = _context.Accomodations.Where(a => a.Id == accomodationId).Include(a => a.Reviews).FirstOrDefault();

            if (ModelState.IsValid)
            {
                
                if (accomodation.Rating == null)
                {
                    accomodation.Rating = (byte)review.Rating;
                }
                else
                {
                    var sum = accomodation.Reviews.Sum(r => r.Rating);
                    sum += review.Rating;
                    accomodation.Rating = (byte?)(sum / (accomodation.Reviews.Count + 1));
                }

                _context.Add(review);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Reviews", new { id = accomodation.Id, name = accomodation.Name });
            }

            //ViewData["AccomodationId"] = new SelectList(_context.Accomodations, "Id", "Id", review.AccomodationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", review.UserId);

            ViewBag.AccomodationId = accomodation.Id;
            ViewBag.AccomodatioName = accomodation.Name;

            return View(accomodation);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id, int accomodationId)
        {
            var accomodation = _context.Accomodations.Where(a => a.Id == accomodationId).Include(a => a.Reviews).FirstOrDefault();

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

            ViewBag.AccomodationId = accomodation.Id;
            ViewBag.AccomodationName = accomodation.Name;

            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int accomodationId, [Bind("Id,UserId,AccomodationId,Rating,Comment")] Review review)
        {
            var accomodation = _context.Accomodations.AsNoTracking().Where(a => a.Id == accomodationId).Include(a => a.Reviews).FirstOrDefault();

            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (accomodation.Rating == null)
                {
                    accomodation.Rating = (byte)review.Rating;
                }
                else
                {
                    var sum = accomodation.Reviews.Where(r => r.Id != id).Sum(r => r.Rating);
                    sum += review.Rating;
                    accomodation.Rating = (byte?)(sum / (accomodation.Reviews.Count));
                }

                _context.Accomodations.Where(a => a.Id == accomodation.Id).ToList().FirstOrDefault().Rating = accomodation.Rating;   


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
                return RedirectToAction("Index", "Reviews", new { id = accomodation.Id, name = accomodation.Name });
            }
            ViewData["AccomodationId"] = new SelectList(_context.Accomodations, "Id", "Id", review.AccomodationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", review.UserId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id, int accomodationId)
        {

            var accomodation = _context.Accomodations.Where(a => a.Id == accomodationId).Include(a => a.Reviews).FirstOrDefault();

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

            ViewBag.AccomodationId = accomodation.Id;
            ViewBag.AccomodationName = accomodation.Name;

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int accomodationId)
        {

            var accomodation = _context.Accomodations.Where(a => a.Id == accomodationId).Include(a => a.Reviews).FirstOrDefault();

            if (_context.Reviews == null)
            {
                return Problem("Entity set 'DBBookingContext.Reviews'  is null.");
            }
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {   
                if (accomodation.Reviews.Count == 1)
                    accomodation.Rating = null;
                else
                {
                    var sum = accomodation.Reviews.Sum(r => r.Rating);
                    sum -= review.Rating;
                    accomodation.Rating = (byte?)(sum / (accomodation.Reviews.Count - 1));
                }


                _context.Reviews.Remove(review);
                
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Reviews", new { id = accomodation.Id, name = accomodation.Name });
        }

        private bool ReviewExists(int id)
        {
          return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
