using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class City
    {
        public City()
        {
            Accomodations = new HashSet<Accomodation>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; } = null!;

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Accomodation> Accomodations { get; set; }
    }
}
