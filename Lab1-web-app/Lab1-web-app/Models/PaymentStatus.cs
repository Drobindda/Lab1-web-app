using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class PaymentStatus
    {
        public PaymentStatus()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
