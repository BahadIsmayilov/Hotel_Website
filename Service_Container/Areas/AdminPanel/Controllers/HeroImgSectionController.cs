using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using Service_Container.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Service_Container.Extensions.IFormFileExtensions;
using static Service_Container.Utulities.Utulitie;
;

namespace Service_Container.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HeroImgSectionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _env;
        public HeroImgSectionController(AppDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.HeroImgSections.ToList());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            HeroImgSection heroImg = await _context.HeroImgSections.FirstOrDefaultAsync();

            if (heroImg == null) return NotFound();

            return View(heroImg);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Create(HeroImgSection heroImg)
        {
            if (!ModelState.IsValid) return View(heroImg);

            if (heroImg.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo should be selected");
                return View(heroImg);
            }

            if (!heroImg.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "File type is not valid");
                return View(heroImg);
            }

            if (!heroImg.Photo.IsLessThan(2))
            {
                ModelState.AddModelError("Photo", "File size cann't be more than 2 mb");
                return View(heroImg);
            }


           string fileName= await heroImg.Photo.Save(_env.WebRootPath, "hero");

            heroImg.Image = fileName;
            await _context.HeroImgSections.AddAsync(heroImg);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            HeroImgSection heroImg = await _context.HeroImgSections.FindAsync(id);

            if (heroImg == null) return NotFound();

            return View(heroImg);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            HeroImgSection heroImg = await _context.HeroImgSections.FindAsync(id);

            RemoveImage(_env.WebRootPath, "hero", heroImg.Image);

            _context.HeroImgSections.Remove(heroImg);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
