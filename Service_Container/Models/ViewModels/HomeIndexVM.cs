using Service_Container.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.ViewModels
{
    public class HomeIndexVM
    {
        public IEnumerable<HeroImgSection> HeroImgSection { get; set; } 
        public HeroTextSection HeroTextSection { get; set; }
        public IEnumerable<ServiceSection> ServiceSections { get; set; }
        public IEnumerable<TestimonialSection> TestimonialSections { get; set; }
        public IEnumerable<BlogsSection> BlogsSections { get; set; }
        public IEnumerable<HomeRoomSection> HomeRoomSections { get; set; }
    }
}
