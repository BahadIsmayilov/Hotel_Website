using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.AboutModels
{
    public class AboutUsImageSection
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string ImageTitle { get; set; }
        public int AboutUsSectionId { get; set; }
        public virtual AboutUsSection AboutUsSection { get; set; }
    }
}
