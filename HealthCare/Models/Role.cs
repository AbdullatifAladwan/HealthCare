using System;
using System.Collections.Generic;

#nullable disable

namespace HealthCare.Models
{
    public partial class Role
    {
        public Role()
        {
            Doctors = new HashSet<Doctor>();
            Users = new HashSet<User>();
        }

        public decimal RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
