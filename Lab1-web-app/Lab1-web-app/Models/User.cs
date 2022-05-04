using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class User
    {
        public User()
        {
            Accomodations = new HashSet<Accomodation>();
            Bookings = new HashSet<Booking>();
            PaymentMethods = new HashSet<PaymentMethod>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int TypeId { get; set; }
        public int CountryId { get; set; }
        public int StatusId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual UserStatus Status { get; set; } = null!;
        public virtual UserType Type { get; set; } = null!;
        public virtual ICollection<Accomodation> Accomodations { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
