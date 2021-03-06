using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1_web_app
{
    [Display(Name = "Номер")]
    public partial class Room
    {
        public Room()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public int AccomodationId { get; set; }
        [Required]
        [Display(Name = "Назва")]
        public string Name { get; set; } = null!;
        [Display(Name = "Стандартна кількість гостів")]
        [Range(1, Int32.MaxValue)]
        public int SdandartOccupancy { get; set; }
        [Display(Name = "Максимальна кількість гостів")]
        [Range(1, Int32.MaxValue)]
        public int MaxOccupancy { get; set; }
        [Display(Name = "Кількість спалень")]
        [Range(1, Int32.MaxValue)]
        public int TotalBedrooms { get; set; }
        [Display(Name = "Кількість ванних кімант")]
        [Range(1, Int32.MaxValue)]
        public int TotalBathrooms { get; set; }
        [Display(Name = "Телевізор")]
        public bool HasTv { get; set; }
        [Display(Name = "Кухня")]
        public bool HasKitchen { get; set; }
        [Display(Name = "Кондиціонер")]
        public bool HasAirCon { get; set; }
        [Display(Name = "Інтернет")]
        public bool HasInternet { get; set; }
        [Display(Name = "Харчування")]
        public int MealServiceId { get; set; }
        [Display(Name = "Опис")]
        public string? Description { get; set; }
        [Display(Name = "Розмір номеру")]
        [Range(0, (double)Decimal.MaxValue)]
        public decimal Size { get; set; }
        [Display(Name = "Ціна за добу")]
        [Range(0, (double)Decimal.MaxValue)]
        public decimal Price { get; set; }
        [Display(Name = "Кількість номерів")]
        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }

        public int StatusId { get; set; }
        [Display(Name = "Житло")]
        public virtual Accomodation Accomodation { get; set; } = null!;
        [Display(Name = "Харчування")]
        public virtual MealService MealService { get; set; } = null!;
        [Display(Name = "Статус")]
        public virtual RoomStatus Status { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
