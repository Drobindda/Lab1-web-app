using System;
using System.Collections.Generic;

namespace Lab1_web_app
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int? CardNumber { get; set; }
        public int? ExpiryDate { get; set; }
        public string? CardHolder { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
