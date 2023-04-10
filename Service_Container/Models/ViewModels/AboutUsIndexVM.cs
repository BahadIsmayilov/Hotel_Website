using Service_Container.Models.AboutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.ViewModels
{
    public class AboutUsIndexVM
    {
        public IEnumerable<AboutUsSection> AboutUsSections { get; set; }
        public AboutTextSection AboutTextSection { get; set; }
        public IEnumerable<GallerySection> GallerySections { get; set; }
    }
}
