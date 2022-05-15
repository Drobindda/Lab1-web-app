﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1_web_app;

namespace Lab1_web_app.Controllers
{
    public class RoomsController : Controller
    {
        private readonly DBBookingContext _context;

        public RoomsController(DBBookingContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(int? id, string name)
        {
            if (id == null) return RedirectToAction("Index", "Accomodations", new {id = _context.Accomodations.Where(e => e.Id == id).FirstOrDefault().TypeId});

            ViewBag.AccomodationId = id;
            ViewBag.AccomodationName = name;

            var dBBookingContext = _context.Rooms.Where(r => r.Id == id).Include(r => r.Accomodation).Include(r => r.MealService).Include(r => r.Status);

            return View(await dBBookingContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Accomodation)
                .Include(r => r.MealService)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create(int accomodationId)
        {
            //ViewData["AccomodationId"] = new SelectList(_context.Accomodations, "Id", "Id");
            ViewData["MealServiceId"] = new SelectList(_context.MealServices, "Id", "Id");
            ViewData["StatusId"] = new SelectList(_context.RoomStatuses, "Id", "Id");

            ViewBag.AccomodationId = accomodationId;
            ViewBag.AccomodationName = _context.Accomodations.Where(a => a.Id == accomodationId).FirstOrDefault().Name;

            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int accomodationId, [Bind("Id,AccomodationId,Name,SdandartOccupancy,MaxOccupancy,TotalBedrooms,TotalBathrooms,HasTv,HasKitchen,HasAirCon,HasInternet,MealServiceId,Description,Size,Price,Quantity,StatusId")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Rooms", new { id = accomodationId, name = _context.Accomodations.Where(a => a.Id == accomodationId).FirstOrDefault().Name });
            }
            //ViewData["AccomodationId"] = new SelectList(_context.Accomodations, "Id", "Id", room.AccomodationId);
            ViewData["MealServiceId"] = new SelectList(_context.MealServices, "Id", "Id", room.MealServiceId);
            ViewData["StatusId"] = new SelectList(_context.RoomStatuses, "Id", "Id", room.StatusId);
            //return View(room);

            return RedirectToAction("Index", "Rooms", new { id = accomodationId, name = _context.Accomodations.Where(a => a.Id == accomodationId).FirstOrDefault().Name });
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["AccomodationId"] = new SelectList(_context.Accomodations, "Id", "Id", room.AccomodationId);
            ViewData["MealServiceId"] = new SelectList(_context.MealServices, "Id", "Id", room.MealServiceId);
            ViewData["StatusId"] = new SelectList(_context.RoomStatuses, "Id", "Id", room.StatusId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccomodationId,Name,SdandartOccupancy,MaxOccupancy,TotalBedrooms,TotalBathrooms,HasTv,HasKitchen,HasAirCon,HasInternet,MealServiceId,Description,Size,Price,Quantity,StatusId")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
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
            ViewData["AccomodationId"] = new SelectList(_context.Accomodations, "Id", "Id", room.AccomodationId);
            ViewData["MealServiceId"] = new SelectList(_context.MealServices, "Id", "Id", room.MealServiceId);
            ViewData["StatusId"] = new SelectList(_context.RoomStatuses, "Id", "Id", room.StatusId);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Accomodation)
                .Include(r => r.MealService)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rooms == null)
            {
                return Problem("Entity set 'DBBookingContext.Rooms'  is null.");
            }
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
          return _context.Rooms.Any(e => e.Id == id);
        }
    }
}