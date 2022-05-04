using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class CancelationStatus
    {
        public CancelationStatus()
        {
            Cancelations = new HashSet<Cancelation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Cancelation> Cancelations { get; set; }
    }
}
