using System;
using System.Collections.Generic;

#nullable disable

namespace HealthCare.Models
{
    public partial class Contactu
    {
        public decimal ContactId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int? Phonenummber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public decimal? WebId { get; set; }

        public virtual Website Web { get; set; }
    }
}
