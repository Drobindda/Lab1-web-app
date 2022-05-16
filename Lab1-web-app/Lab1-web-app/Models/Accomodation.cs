using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1_web_app
{
    public partial class Accomodation
    {
        public Accomodation()
        {
            Reviews = new HashSet<Review>();
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
        public int TypeId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public byte? Stars { get; set; }
        public byte? Rating { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        public int StatusId { get; set; }
        public string? Description { get; set; }
        public float? Longtitude { get; set; }
        public float? Latitude { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual AccomodationStatus Status { get; set; } = null!;
        public virtual AccomodationType Type { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
