using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccomodationId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;

        public virtual Accomodation Accomodation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
