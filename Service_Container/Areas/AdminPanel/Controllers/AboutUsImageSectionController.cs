using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using Service_Container.Models.AboutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Service_Container.Extensions.IFormFileExtensions;
using static Service_Container.Utulities.Utulitie;

namespace Service_Container.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AboutUsImageSectionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _env;

        public AboutUsImageSectionController(AppDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.AboutUsImageSections.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            AboutUsImageSection aboutUsImage = await _context.AboutUsImageSections
                                                             .FirstOrDefaultAsync(x=>x.Id==id);

            if (aboutUsImage == null) return NotFound();

            return View(aboutUsImage);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutUsImageSection aboutUsImage)
        {
            if (!ModelState.IsValid) return View(aboutUsImage);

            if (aboutUsImage.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo should be selected");
                return View(aboutUsImage);
            }
            if (!aboutUsImage.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "File type is not valid");
                return View(aboutUsImage);
            }
            if (aboutUsImage.Photo.IsLessThan(2))
            {
                ModelState.AddModelError("Photo", "File size cann't more than 2mb");
                return View(aboutUsImage);
            }

            string fileName = await aboutUsImage.Photo.Save(_env.WebRootPath,"about");
            aboutUsImage.Image = fileName;

            await _context.AboutUsImageSections.AddAsync(aboutUsImage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            AboutUsImageSection aboutUsImage = await _context.AboutUsImageSections
                                                             .FindAsync(id);

            if (aboutUsImage == null) return NotFound();

            return View(aboutUsImage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutUsImageSection aboutUsImage)
        {
            if (!ModelState.IsValid) return View(aboutUsImage);

            if (id == null) return NotFound();

            AboutUsImageSection aboutUsImageDb = await _context.AboutUsImageSections
                                                             .FindAsync(id);

            if (aboutUsImageDb == null) return NotFound();

            if (aboutUsImage.Photo != null)
            {
                if (!aboutUsImage.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "File type is not valid");
                    return View(aboutUsImage);
                }
                if (aboutUsImage.Photo.IsLessThan(2))
                {
                    ModelState.AddModelError("Photo", "File size cann't more than 2mb");
                    return View(aboutUsImage);
                }

                RemoveImage(_env.WebRootPath, "about", aboutUsImageDb.Image);
                aboutUsImageDb.Image = await aboutUsImage.Photo.Save(_env.WebRootPath, "about");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            AboutUsImageSection aboutUsImage = await _context.AboutUsImageSections
                                                             .FindAsync(id);

            if (aboutUsImage == null) return NotFound();

            return View(aboutUsImage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();

            AboutUsImageSection aboutUsImage = await _context.AboutUsImageSections
                                                             .FindAsync(id);

            if (aboutUsImage == null) return NotFound();

            RemoveImage(_env.WebRootPath, "blog", aboutUsImage.Image);

            _context.AboutUsImageSections.Remove(aboutUsImage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
