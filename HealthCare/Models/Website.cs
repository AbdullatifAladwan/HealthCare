using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HealthCare.Models
{
    public partial class Website
    {
        public Website()
        {
            Aboutus = new HashSet<Aboutu>();
            Contactus = new HashSet<Contactu>();
            Reviews = new HashSet<Review>();
            Testimonials = new HashSet<Testimonial>();
        }

        public decimal WebId { get; set; }
        public string BackgroundImage { get; set; }
        public string LogoPic { get; set; }
        public string SlidrPic { get; set; }
        [NotMapped]// don't go and fetch something a like it in database because it is undefiened
        public IFormFile ImageFile { get; set; }// represent a file with http req{mostly we use the prop FileName to get the file we need

        public string Pargraph { get; set; }
        public decimal? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Aboutu> Aboutus { get; set; }
        public virtual ICollection<Contactu> Contactus { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
