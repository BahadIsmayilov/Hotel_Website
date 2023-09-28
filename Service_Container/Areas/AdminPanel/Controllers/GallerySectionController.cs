using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using Service_Container.Models.AboutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class GallerySectionController : Controller
    {
        private readonly AppDbContext _context;

        public GallerySectionController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.GallerySections.FirstOrDefaultAsync());
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            GallerySection gallerySection = await _context.GallerySections.FindAsync(id);

            if (gallerySection == null) return NotFound();

            return View(gallerySection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, GallerySection gallerySection)
        {
            if (!ModelState.IsValid) return View(gallerySection);

            GallerySection gallerySectionDb = await _context.GallerySections.FindAsync(id);


            gallerySectionDb.Title = gallerySection.Title;
            gallerySectionDb.SubTitle = gallerySection.SubTitle;
            gallerySectionDb.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
