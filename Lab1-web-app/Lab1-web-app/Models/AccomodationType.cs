using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1_web_app
{
    public partial class AccomodationType
    {
        public AccomodationType()
        {
            Accomodations = new HashSet<Accomodation>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Тип житла")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Accomodation> Accomodations { get; set; }
    }
}
