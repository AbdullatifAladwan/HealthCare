using System;
using System.Collections.Generic;

#nullable disable

namespace HealthCare.Models
{
    public partial class Creditcard
    {
        public Creditcard()
        {
            Appointments = new HashSet<Appointment>();
        }

        public decimal CardId { get; set; }
        public decimal CardNumber { get; set; }
        public string Ccv { get; set; }
        public decimal Balance { get; set; }
        public DateTime? ExpireData { get; set; }
        public decimal? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
