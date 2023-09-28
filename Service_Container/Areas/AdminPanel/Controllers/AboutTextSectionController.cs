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
    public class AboutTextSectionController : Controller
    {
        private readonly AppDbContext _context;
        public AboutTextSectionController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.AboutTextSections.FirstOrDefaultAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            AboutTextSection aboutUsText = await _context.AboutTextSections.FindAsync(id);

            if (aboutUsText == null) return NotFound();

            return View(aboutUsText);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutTextSection aboutUsText)
        {
            if (!ModelState.IsValid) return View(aboutUsText);

            await _context.AboutTextSections.AddAsync(aboutUsText);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            AboutTextSection aboutUsText = await _context.AboutTextSections.FindAsync(id);

            if (aboutUsText == null) return NotFound();

            return View(aboutUsText);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutTextSection aboutTextSection)
        {
            if (!ModelState.IsValid) return View(aboutTextSection);

            if (id == null) return NotFound();

            AboutTextSection aboutUsTextDb = await _context.AboutTextSections.FindAsync(id);

            if (aboutUsTextDb == null) return NotFound();

            aboutUsTextDb.Title = aboutTextSection.Title;
            aboutUsTextDb.AboutCompanyActivity = aboutTextSection.AboutCompanyActivity;
            aboutUsTextDb.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            AboutTextSection aboutUsText = await _context.AboutTextSections.FindAsync(id);

            if (aboutUsText == null) return NotFound();

            return View(aboutUsText);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();

            AboutTextSection aboutUsText = await _context.AboutTextSections.FindAsync(id);

            if (aboutUsText == null) return NotFound();

            _context.AboutTextSections.Remove(aboutUsText);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
