using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1_web_app
{
    [Display(Name = "Відгук")]
    public partial class Review
    {
        public int Id { get; set; }
        [Display(Name = "Користувач")]
        public int UserId { get; set; }
        public int AccomodationId { get; set; }
        [Display(Name = "Рейтинг")]
        [Range(0, 100)]
        public int Rating { get; set; }
        [Display(Name = "Коментар")]
        public string Comment { get; set; } = null!;
        [Display(Name = "Житло")]
        public virtual Accomodation Accomodation { get; set; } = null!;
        [Display(Name = "Користувач")]
        public virtual User User { get; set; } = null!;
    }
}
