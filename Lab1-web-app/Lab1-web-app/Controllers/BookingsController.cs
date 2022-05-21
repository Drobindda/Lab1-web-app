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
    public class BookingsController : Controller
    {
        private readonly DBBookingContext _context;

        public BookingsController(DBBookingContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var dBBookingContext = _context.Bookings.Include(b => b.Room).ThenInclude(r => r.Accomodation).Include(b => b.Status).Include(b => b.User);
            return View(await dBBookingContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Status)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create(int roomId)
        {

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            
            var room = _context.Rooms.Where(r => r.Id == roomId).Include(r => r.Accomodation).FirstOrDefault();

            ViewBag.RoomPrice = room.Price;
            ViewBag.RoomId = room.Id;
            ViewBag.RoomName = room.Name;
            ViewBag.AccomodationId = room.Accomodation.Id;
            ViewBag.AccomodationName = room.Accomodation.Name;

            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int roomId, [Bind("Id,UserId,RoomId,StatusId,StartDate,EndDate,GuestQuantity,Price,CreatedAt,UpdatedAt")] Booking booking)
        {
            var accomodation = _context.Rooms.Where(r => r.Id == roomId).Include(r => r.Accomodation).FirstOrDefault().Accomodation;
            var room = _context.Rooms.Where(r => r.Id == roomId).Include(r => r.Accomodation).FirstOrDefault();

            booking.CreatedAt = DateTime.Now;
            booking.UpdatedAt = booking.CreatedAt;
            int duration = (int)(booking.EndDate - booking.StartDate).TotalDays;
            booking.StatusId = _context.BookingStatuses.Where(bs => bs.Name == "OK").First().Id;
            booking.Price = (decimal)(duration * _context.Rooms.Where(r => r.Id == booking.RoomId).First().Price);

            if(booking.GuestQuantity > room.MaxOccupancy)
            {
                ModelState.AddModelError("GuestQuantity", $"Кількість гостей перевищує максимально допустиму: {room.MaxOccupancy}");
            }

            if((booking.EndDate - booking.StartDate).Days < 1)
            {
                ModelState.AddModelError("EndDate", "Кількість днів проживання не може бути менше одного");
            }

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index", "Rooms", new { id = accomodation.Id, name = accomodation.Name });
            }
            //ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", booking.RoomId);
            //ViewData["StatusId"] = new SelectList(_context.BookingStatuses, "Id", "Id", booking.StatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", booking.UserId);

            ViewBag.RoomPrice = room.Price;
            ViewBag.RoomId = room.Id;
            ViewBag.RoomName = room.Name;
            ViewBag.AccomodationId = room.Accomodation.Id;
            ViewBag.AccomodationName = room.Accomodation.Name;

            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,RoomId,StatusId,StartDate,EndDate,GuestQuantity,Price,CreatedAt,UpdatedAt")] Booking booking)
        {
            var bookingOld = _context.Bookings.AsNoTracking().Include(b => b.Room).Where(b => b.Id == id).First();
            var room = _context.Rooms.Where(r => r.Id == bookingOld.RoomId).Include(r => r.Accomodation).FirstOrDefault();


            booking.RoomId = bookingOld.RoomId;
            booking.StatusId = bookingOld.StatusId;
            booking.UserId = bookingOld.UserId;
            booking.CreatedAt = bookingOld.CreatedAt;
            booking.UpdatedAt = DateTime.Now;

            int duration = (int)(booking.EndDate - booking.StartDate).TotalDays;
            booking.Price = (decimal)(duration * bookingOld.Room.Price);


            if (id != booking.Id)
            {
                return NotFound();
            }

            if (booking.GuestQuantity > room.MaxOccupancy)
            {
                ModelState.AddModelError("GuestQuantity", $"Кількість гостей перевищує максимально допустиму: {room.MaxOccupancy}");
            }

            if ((booking.EndDate - booking.StartDate).Days < 1)
            {
                ModelState.AddModelError("EndDate", "Кількість днів проживання не може бути менше одного");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            //ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", booking.RoomId);
            //ViewData["StatusId"] = new SelectList(_context.BookingStatuses, "Id", "Id", booking.StatusId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", booking.UserId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Status)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'DBBookingContext.Bookings'  is null.");
            }
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
          return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
