using System;
using System.Collections.Generic;

#nullable disable

namespace HealthCare.Models
{
    public partial class Appointment
    {
        public decimal AppointmentId { get; set; }
        public string Fullname { get; set; }
        public DateTime Data { get; set; }
        public string Time { get; set; }
        public int Phonenumber { get; set; }
        public string Massage { get; set; }
        public bool? AcceptOrReject { get; set; }
        public decimal? DoctorId { get; set; }
        public decimal? UserId { get; set; }
        public decimal? CardId { get; set; }

        public virtual Creditcard Card { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual User User { get; set; }
    }
}
