using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class Booking
    {
        public Booking()
        {
            Cancelations = new HashSet<Cancelation>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestQuantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Room Room { get; set; } = null!;
        public virtual BookingStatus Status { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Cancelation> Cancelations { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
