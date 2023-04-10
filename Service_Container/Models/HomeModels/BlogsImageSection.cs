using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models
{
    public class BlogsImageSection
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int BlogsSectionId { get; set; }
        public virtual BlogsSection BlogsSection { get; set; }
    }
}
