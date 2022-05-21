using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1_web_app
{
    [Display(Name = "Бронювання")]
    public partial class Booking
    {
        public Booking()
        {
            Cancelations = new HashSet<Cancelation>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        [Display(Name = "Користувач")]
        public int UserId { get; set; }
        [Display(Name = "Номер")]
        public int RoomId { get; set; }
        [Display(Name = "Статус")]
        public int StatusId { get; set; }
        [Display(Name = "Дата заїзду")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "Дата виїзду")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Display(Name = "Кількість гостей")]
        [Range(1, Int32.MaxValue)]
        public int GuestQuantity { get; set; }
        [Display(Name = "Загальна ціна")]
        public decimal Price { get; set; }
        [Display(Name = "Створено")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Оновлено")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Номер")]
        public virtual Room Room { get; set; } = null!;
        [Display(Name = "Статус")]
        public virtual BookingStatus Status { get; set; } = null!;
        [Display(Name = "Користувач")]
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Cancelation> Cancelations { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
