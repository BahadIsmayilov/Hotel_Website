using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using Service_Container.Models.AboutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Service_Container.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AboutUsSectionController : Controller
    {
        private readonly AppDbContext _context;
        public AboutUsSectionController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View( await _context.AboutUsSections.FirstOrDefaultAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            AboutUsSection aboutUs = await _context.AboutUsSections.FindAsync(id);

            if (aboutUs == null) return NotFound();

            return View(aboutUs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutUsSection aboutUs)
        {
            if (!ModelState.IsValid) return View(aboutUs);

            await _context.AboutUsSections.AddAsync(aboutUs);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            AboutUsSection aboutUs = await _context.AboutUsSections.FindAsync(id);

            if (aboutUs == null) return NotFound();

            return View(aboutUs);
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,AboutUsSection aboutUs)
        {
            if (!ModelState.IsValid) return View(aboutUs);

            AboutUsSection aboutUsDb = await _context.AboutUsSections.FindAsync(id);

            if (aboutUs == null) return NotFound();

            aboutUsDb.Title = aboutUs.Title;
            aboutUsDb.SmallDescription = aboutUs.SmallDescription;
            aboutUs.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            AboutUsSection aboutUs = await _context.AboutUsSections.FindAsync(id);

            if (aboutUs == null) return NotFound();

            return View(aboutUs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            AboutUsSection aboutUs = await _context.AboutUsSections.FindAsync(id);
            if (aboutUs == null) return NotFound();

            _context.AboutUsSections.Remove(aboutUs);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
