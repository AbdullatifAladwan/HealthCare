using System;
using System.Collections.Generic;

#nullable disable

namespace HealthCare.Models
{
    public partial class Attendance
    {
        public decimal AttendanceId { get; set; }
        public string Time { get; set; }
        public string Day { get; set; }
        public string DoctorName { get; set; }
        public decimal? DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
