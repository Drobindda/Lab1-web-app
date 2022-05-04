using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneCode { get; set; } = null!;
        public string CountryCode { get; set; } = null!;

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
