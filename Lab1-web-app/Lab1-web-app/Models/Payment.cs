using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int PaymentMethodId { get; set; }
        public int BookingId { get; set; }
        public int StatusId { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual Booking Booking { get; set; } = null!;
        public virtual PaymentMethod PaymentMethod { get; set; } = null!;
        public virtual PaymentStatus Status { get; set; } = null!;
    }
}
