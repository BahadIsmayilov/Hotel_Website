using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models
{
    public class TestimonialSection
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string User { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public DateTime? Update { get; set; }
    }
}
