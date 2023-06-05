using System;
using System.Collections.Generic;

#nullable disable

namespace HealthCare.Models
{
    public partial class Specialization
    {
        public Specialization()
        {
            Doctors = new HashSet<Doctor>();
        }

        public decimal SpecializationId { get; set; }
        public string Specialization1 { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
