using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class BookingStatus
    {
        public BookingStatus()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
