using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HealthCare.Models
{
    public partial class Testimonial
    {
        public decimal TestId { get; set; }
        public string Name { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]// don't go and fetch something a like it in database because it is undefiened
        public IFormFile ImageFile { get; set; }// represent a file with http req{mostly we use the prop FileName to get the file we need

        public string Feedback { get; set; }
        public decimal? WebId { get; set; }

        public virtual Website Web { get; set; }
    }
}
