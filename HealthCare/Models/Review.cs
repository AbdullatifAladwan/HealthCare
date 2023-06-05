using System;
using System.Collections.Generic;

#nullable disable

namespace HealthCare.Models
{
    public partial class Review
    {
        public decimal ReviewId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? WebId { get; set; }

        public virtual Website Web { get; set; }
    }
}
