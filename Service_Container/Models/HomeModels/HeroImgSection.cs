using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models
{
    public class HeroImgSection
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public DateTime? UpdateDate { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
