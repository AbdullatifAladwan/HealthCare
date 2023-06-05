using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HealthCare.Models
{
    public partial class Aboutu
    {
        public decimal AboutusId { get; set; }
        public string BackgroundImage { get; set; }
        [NotMapped]// don't go and fetch something a like it in database because it is undefiened
        public IFormFile ImageFile { get; set; }// represent a file with http req{mostly we use the prop FileName to get the file we need

        public string Description { get; set; }
        public string Pargraph1 { get; set; }
        public string Pargraph2 { get; set; }
        public string Pargraph3 { get; set; }
        public decimal? WebId { get; set; }

        public virtual Website Web { get; set; }
    }
}
