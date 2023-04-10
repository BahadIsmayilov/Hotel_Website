using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models
{
    public class BlogsSection
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string TravelPlace { get; set; }
        public DateTime TravelDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdaetDate { get; set; }
        public virtual ICollection<BlogsImageSection> BlogsImageSections { get; set; }
    }
}
