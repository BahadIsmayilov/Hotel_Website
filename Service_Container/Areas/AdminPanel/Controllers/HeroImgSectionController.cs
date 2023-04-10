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
//using static Service_Container

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

            if (!heroImg.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "File type is not valid");
                return View(heroImg);
            }

            if (heroImg.Photo.Length / 1024 / 1024 > 2)
            {
                ModelState.AddModelError("Photo", "File size cann't be more than 2 mb");
                return View(heroImg);
            }


            string path = Path.Combine(_env.WebRootPath,"img");

            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Guid.NewGuid().ToString() + heroImg.Photo.FileName;
            string fileNameWithFolder = Path.Combine("hero" , fileName);


            string resultPath = Path.Combine(path, fileNameWithFolder);
            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create)) {
            await heroImg.Photo.CopyToAsync(fileStream);
            };

            heroImg.Image = fileName;
            await _context.HeroImgSections.AddAsync(heroImg);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
