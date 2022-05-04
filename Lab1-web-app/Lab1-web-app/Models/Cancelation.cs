using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class Cancelation
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public DateTime CancelationDate { get; set; }
        public int StatusId { get; set; }

        public virtual Booking Booking { get; set; } = null!;
        public virtual CancelationStatus Status { get; set; } = null!;
    }
}
