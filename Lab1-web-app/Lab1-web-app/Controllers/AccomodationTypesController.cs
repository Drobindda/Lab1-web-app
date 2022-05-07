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
    public class AccomodationTypesController : Controller
    {
        private readonly DBBookingContext _context;

        public AccomodationTypesController(DBBookingContext context)
        {
            _context = context;
        }

        // GET: AccomodationTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccomodationTypes.ToListAsync());
        }

        // GET: AccomodationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accomodationType = await _context.AccomodationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accomodationType == null)
            {
                return NotFound();
            }

            //return View(accomodationType);
            return RedirectToAction("Index", "Accomodations", new { id = accomodationType.Id, name = accomodationType.Name });
        }

        // GET: AccomodationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccomodationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AccomodationType accomodationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accomodationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accomodationType);
        }

        // GET: AccomodationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accomodationType = await _context.AccomodationTypes.FindAsync(id);
            if (accomodationType == null)
            {
                return NotFound();
            }
            return View(accomodationType);
        }

        // POST: AccomodationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AccomodationType accomodationType)
        {
            if (id != accomodationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accomodationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccomodationTypeExists(accomodationType.Id))
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
            return View(accomodationType);
        }

        // GET: AccomodationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accomodationType = await _context.AccomodationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accomodationType == null)
            {
                return NotFound();
            }

            return View(accomodationType);
        }

        // POST: AccomodationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accomodationType = await _context.AccomodationTypes.FindAsync(id);
            _context.AccomodationTypes.Remove(accomodationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccomodationTypeExists(int id)
        {
            return _context.AccomodationTypes.Any(e => e.Id == id);
        }
    }
}
