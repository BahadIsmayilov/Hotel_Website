using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using Service_Container.Models;
using Service_Container.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeIndexVM heroSectionVM = new HomeIndexVM
            {
                HeroImgSection = _context.HeroImgSections.ToList(),
                HeroTextSection = _context.HeroTextSection.FirstOrDefault(),
                ServiceSections = _context.ServiceSections.ToList(),
                TestimonialSections = _context.TestimonialSection.ToList(),
                BlogsSections = _context.BlogsSections.Include(p => p.BlogsImageSections).OrderByDescending(p => p.Id).Take(5),
                HomeRoomSections = _context.HomeRoomSections.Include(p => p.HomeImageRoomSections).Include(p=>p.HomeRoomCategorySection).Take(4)
            };
            return View(heroSectionVM);
        }
        public async Task<IActionResult> RoomDetails(int? id)
        {
            if(id==null) return NotFound();

            HomeRoomSection homeRoom =await _context.HomeRoomSections.Include(x=>x.HomeImageRoomSections).FirstOrDefaultAsync(x=>x.Id==id);

            if (homeRoom == null) return NotFound();

            return View(homeRoom);
        }
    }
}
