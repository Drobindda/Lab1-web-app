using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1_web_app
{
    public partial class Room
    {
        public Room()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public int AccomodationId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public int SdandartOccupancy { get; set; }
        public int MaxOccupancy { get; set; }
        public int TotalBedrooms { get; set; }
        public int TotalBathrooms { get; set; }
        public bool HasTv { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasAirCon { get; set; }
        public bool HasInternet { get; set; }
        public int MealServiceId { get; set; }
        public string? Description { get; set; }
        public decimal Size { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int StatusId { get; set; }

        public virtual Accomodation Accomodation { get; set; } = null!;
        public virtual MealService MealService { get; set; } = null!;
        public virtual RoomStatus Status { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
