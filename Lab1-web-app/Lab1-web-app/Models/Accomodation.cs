using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1_web_app
{

    [Display(Name = "Житло")]
    public partial class Accomodation
    {
        public Accomodation()
        {
            Reviews = new HashSet<Review>();
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        [Display(Name = "Користувач")]
        public int UserId { get; set; }
        [Display(Name = "Місто")]
        public int CityId { get; set; }
        [Display(Name = "Тип")]
        public int TypeId { get; set; }
        [Required]
        [Display(Name = "Назва")]
        public string Name { get; set; } = null!;
        [Display(Name = "Зірки")]
        [Range(0,5)]
        public byte? Stars { get; set; }
        [Display(Name = "Рейтинг")]
        public byte? Rating { get; set; }
        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Неправильно введений номер телефону")]
        public string? Phone { get; set; }
        [Display(Name = "Створено")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Оновлено")]
        public DateTime UpdatedAt { get; set; }
        [Required]
        [Display(Name = "Адреса")]
        public string Address { get; set; } = null!;
        public int StatusId { get; set; }
        [Display(Name = "Опис")]
        public string? Description { get; set; }
        [Display(Name = "Довгота")]
        public float? Longtitude { get; set; }
        [Display(Name = "Широта")]
        public float? Latitude { get; set; }

        [Display(Name = "Місто")]
        public virtual City City { get; set; } = null!;
        public virtual AccomodationStatus Status { get; set; } = null!;
        [Display(Name = "Тип")]
        public virtual AccomodationType Type { get; set; } = null!;
        [Display(Name = "Користувач")]
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
