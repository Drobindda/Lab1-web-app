#nullable disable
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
    public class AccomodationsController : Controller
    {
        private readonly DBBookingContext _context;

        public AccomodationsController(DBBookingContext context)
        {
            _context = context;
        }

        // GET: Accomodations
        public async Task<IActionResult> Index(int? id, string name)
        {
            if(id == null) return RedirectToAction("Index", "AccomodationTypes");

            ViewBag.AccomodationTypeId = id;
            ViewBag.AccomodationTypeName = _context.AccomodationTypes.Where(at => at.Id == id).FirstOrDefault().Name;    
            var dBBookingContext = _context.Accomodations.Where(a => a.TypeId == id).Include(a => a.City).Include(a => a.Status).Include(a => a.Type).Include(a => a.User);

            return View(await dBBookingContext.ToListAsync());
        }

        // GET: Accomodations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accomodation = await _context.Accomodations
                .Include(a => a.City)
                .Include(a => a.Status)
                .Include(a => a.Type)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accomodation == null)
            {
                return NotFound();
            }

            return View(accomodation);
        }

        // GET: Accomodations/Create
        public IActionResult Create(int typeId)
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");

            ViewBag.AccomodationTypeId = typeId;
            ViewBag.AccomodationTypeName = _context.AccomodationTypes.Where(a => a.Id == typeId).FirstOrDefault().Name;

            return View();
        }

        // POST: Accomodations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int typeId, [Bind("Id,UserId,CityId,TypeId,Name,Stars,Rating,Phone,CreatedAt,UpdatedAt,Address,StatusId,Description,Longtitude,Latitude")] Accomodation accomodation)
        {

            accomodation.CreatedAt = DateTime.Now;
            accomodation.UpdatedAt = DateTime.Now;
            accomodation.StatusId = _context.AccomodationStatuses.Where(s => s.Name == "OK").FirstOrDefault().Id;


            if (ModelState.IsValid)
            {
                _context.Add(accomodation);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Accomodations", new { id = typeId, name = _context.AccomodationTypes.Where(a => a.Id == typeId).FirstOrDefault().Name});
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", accomodation.CityId);
            ViewData["StatusId"] = new SelectList(_context.AccomodationStatuses, "Id", "Id", accomodation.StatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", accomodation.UserId);

            return RedirectToAction("Index", "Accomodations", new { id = typeId, name = _context.AccomodationTypes.Where(a => a.Id == typeId).FirstOrDefault().Name });

            //return View(accomodation);
        }

        // GET: Accomodations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accomodation = await _context.Accomodations.FindAsync(id);
            if (accomodation == null)
            {
                return NotFound();
            }

            ViewBag.AccomodationId = accomodation.Id;
            ViewBag.AccomodationName = accomodation.Name;

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", accomodation.UserId);
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", accomodation.CityId);
            ViewData["TypeId"] = new SelectList(_context.AccomodationTypes, "Id", "Name", accomodation.TypeId);

            return View(accomodation);
        }

        // POST: Accomodations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CityId,TypeId,Name,Stars,Rating,Phone,CreatedAt,UpdatedAt,Address,StatusId,Description,Longtitude,Latitude")] Accomodation accomodation)
        {
            if (id != accomodation.Id)
            {
                return NotFound();
            }

            
             var acc = await _context.Accomodations.AsNoTracking().Where(a => a.Id == id).Include(a => a.Type).FirstOrDefaultAsync();

             accomodation.Rating = acc.Rating;
             accomodation.CreatedAt = acc.CreatedAt;
             accomodation.UpdatedAt = DateTime.Now;
             accomodation.StatusId = acc.StatusId;
            


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accomodation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccomodationExists(accomodation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Accomodations", new { id = acc.TypeId, name = acc.Type.Name});
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", accomodation.CityId);
            ViewData["StatusId"] = new SelectList(_context.AccomodationStatuses, "Id", "Id", accomodation.StatusId);
            ViewData["TypeId"] = new SelectList(_context.AccomodationTypes, "Id", "Name", accomodation.TypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", accomodation.UserId);
            return View(accomodation);
        }

        // GET: Accomodations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accomodation = await _context.Accomodations
                .Include(a => a.City)
                .Include(a => a.Status)
                .Include(a => a.Type)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accomodation == null)
            {
                return NotFound();
            }

            return View(accomodation);
        }

        // POST: Accomodations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accomodation = await _context.Accomodations.FindAsync(id);
            _context.Accomodations.Remove(accomodation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccomodationExists(int id)
        {
            return _context.Accomodations.Any(e => e.Id == id);
        }
    }
}
