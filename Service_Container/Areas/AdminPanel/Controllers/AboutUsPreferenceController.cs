using Microsoft.AspNetCore.Mvc;
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
    public class AboutUsPreferenceController : Controller
    {
        private readonly AppDbContext _context;
        public AboutUsPreferenceController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.AboutUsPreferncesSections.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            AboutUsPreferncesSection aboutUsPrefernces = await _context.AboutUsPreferncesSections.FindAsync(id);

            if (aboutUsPrefernces == null) return NotFound();

            return View(aboutUsPrefernces);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutUsPreferncesSection aboutUsPrefernces)
        {
            if (!ModelState.IsValid) return View(aboutUsPrefernces);

            await _context.AboutUsPreferncesSections.AddAsync(aboutUsPrefernces);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            AboutUsPreferncesSection aboutUsPrefernces = await _context.AboutUsPreferncesSections.FindAsync(id);

            if (aboutUsPrefernces == null) return NotFound();

            return View(aboutUsPrefernces);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,AboutUsPreferncesSection aboutUsPreference)
        {
            if (!ModelState.IsValid) return View(aboutUsPreference);

            if (id == null) return NotFound();

            AboutUsPreferncesSection aboutUsPrefernceDb = await _context.AboutUsPreferncesSections.FindAsync(id);

            if (aboutUsPrefernceDb == null) return NotFound();

            aboutUsPrefernceDb.Prefernce = aboutUsPreference.Prefernce;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            AboutUsPreferncesSection aboutUsPrefernceDb = await _context.AboutUsPreferncesSections.FindAsync(id);

            if (aboutUsPrefernceDb == null) return NotFound();

            return View(aboutUsPrefernceDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            AboutUsPreferncesSection aboutUsPrefernceDb = await _context.AboutUsPreferncesSections.FindAsync(id);

            if (aboutUsPrefernceDb == null) return NotFound();

            _context.AboutUsPreferncesSections.Remove(aboutUsPrefernceDb);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
