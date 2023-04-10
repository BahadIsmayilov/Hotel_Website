using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using Service_Container.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly AppDbContext _context;
        public AboutUsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AboutUsIndexVM aboutUs = new AboutUsIndexVM()
            {
                AboutUsSections = _context.AboutUsSections.Include(x => x.AboutUsPreferncesSections).Include(x => x.AboutUsImageSections).OrderByDescending(x => x.Id),
                AboutTextSection = _context.AboutTextSections.FirstOrDefault(),
                GallerySections = _context.GallerySections.Include(x => x.GalleryImageSections).OrderByDescending(x => x.Id).Take(4)
            };
            return View(aboutUs);
        }
    }
}
