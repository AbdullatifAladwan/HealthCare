using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HealthCare.Models
{
    public partial class User
    {
        public User()
        {
            Appointments = new HashSet<Appointment>();
            Creditcards = new HashSet<Creditcard>();
            Websites = new HashSet<Website>();
        }

        public decimal UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]// don't go and fetch something a like it in database because it is undefiened
        public IFormFile ImageFile { get; set; }// represent a file with http req{mostly we use the prop FileName to get the file we need

        public string Password { get; set; }
        public int? Phonenumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Creditcard> Creditcards { get; set; }
        public virtual ICollection<Website> Websites { get; set; }
    }
}
