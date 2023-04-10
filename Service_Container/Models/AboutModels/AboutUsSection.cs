using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.AboutModels
{
    public class AboutUsSection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SmallDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public virtual ICollection<AboutUsPreferncesSection> AboutUsPreferncesSections { get; set; }
        public virtual ICollection<AboutUsImageSection> AboutUsImageSections { get; set; }
    }
}
