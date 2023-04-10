using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.AboutModels
{
    public class GalleryImageSection
    {
        public int Id { get; set; }
        public string ImageTitle { get; set; }
        public string Image { get; set; }
        public int GallerSectionId { get; set; }
        public virtual GallerySection GallerySection { get; set; }
    }
}
