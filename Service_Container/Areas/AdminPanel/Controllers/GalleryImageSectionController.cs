using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service_Container.DAL;
using Service_Container.Models.AboutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using static Service_Container.Extensions.IFormFileExtensions;
using static Service_Container.Utulities.Utulitie;

namespace Service_Container.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class GalleryImageSectionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _env;

        public GalleryImageSectionController(AppDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.GalleryImageSections.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            GalleryImageSection galleryImage = await _context.GalleryImageSections
                                                             .FindAsync(id);

            if (galleryImage == null) return NotFound();

            return View(galleryImage);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GalleryImageSection galleryImage)
        {
            if (!ModelState.IsValid) return View(galleryImage);

            if (galleryImage.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo should be selected");
                return View(galleryImage);
            }
            if (!galleryImage.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "File type is not valid");
                return View(galleryImage);
            }
            if (galleryImage.Photo.IsLessThan(2))
            {
                ModelState.AddModelError("Photo", "File size cann't more than 2mb");
                return View(galleryImage);
            }

            string fileName = await galleryImage.Photo.Save(_env.WebRootPath, "gallery");
            galleryImage.Image = fileName;

            await _context.GalleryImageSections.AddAsync(galleryImage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            GalleryImageSection galleryImageDb = await _context.GalleryImageSections
                                                              .FindAsync(id);

            if (galleryImageDb == null) return NotFound();

            return View(galleryImageDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, GalleryImageSection galleryImage)
        {
            if (!ModelState.IsValid) return View(galleryImage);

            if (id == null) return NotFound();

            GalleryImageSection galleryImageDb = await _context.GalleryImageSections
                                                              .FindAsync(id);

            if (galleryImageDb == null) return NotFound();

            if (galleryImage.Photo != null)
            {
                if (!galleryImage.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "File type is not valid");
                    return View(galleryImage);
                }
                if (galleryImage.Photo.IsLessThan(2))
                {
                    ModelState.AddModelError("Photo", "File size cann't more than 2mb");
                    return View(galleryImage);
                }

                RemoveImage(_env.WebRootPath, "gallery", galleryImageDb.Image);
                galleryImageDb.Image = await galleryImage.Photo.Save(_env.WebRootPath, "gallery");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            GalleryImageSection galleryImage = await _context.GalleryImageSections
                                                             .FindAsync(id);

            if (galleryImage == null) return NotFound();

            return View(galleryImage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();

            GalleryImageSection galleryImage = await _context.GalleryImageSections
                                                             .FindAsync(id);

            if (galleryImage == null) return NotFound();

            RemoveImage(_env.WebRootPath, "gallery", galleryImage.Image);

            _context.GalleryImageSections.Remove(galleryImage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
