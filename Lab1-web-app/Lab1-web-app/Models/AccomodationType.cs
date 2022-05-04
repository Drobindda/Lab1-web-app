using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class AccomodationType
    {
        public AccomodationType()
        {
            Accomodations = new HashSet<Accomodation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Accomodation> Accomodations { get; set; }
    }
}
