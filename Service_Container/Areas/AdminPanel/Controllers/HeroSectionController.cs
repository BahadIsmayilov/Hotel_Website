using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using Service_Container.Models;
using Service_Container.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HeroSectionController : Controller
    {
        //private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public HeroSectionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.HeroTextSection.FirstOrDefault());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            HeroTextSection heroText =await _context.HeroTextSection.FindAsync(id);

            if (heroText == null) return NotFound();

            return View(heroText);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (HeroTextSection heroText)
        {
            if (!ModelState.IsValid) return View(heroText);

            await _context.HeroTextSection.AddAsync(heroText);
            await _context.SaveChangesAsync();

            return RedirectToAction (nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            HeroTextSection heroText = await _context.HeroTextSection.FindAsync(id);

            if (heroText == null) return NotFound();

            return View(heroText);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, HeroTextSection heroText)
        {
            if (!ModelState.IsValid) return View(heroText);

            HeroTextSection hero = await _context.HeroTextSection.FindAsync(id);

            DateTime update = DateTime.Now;

            hero.Title = heroText.Title;
            hero.Description = heroText.Description;
            hero.UpdateDate = update;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            HeroTextSection heroText = await _context.HeroTextSection.FindAsync(id);
            if (heroText == null) return NotFound();

            return View(heroText);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            HeroTextSection heroText = await _context.HeroTextSection.FindAsync(id);
            if (heroText == null) return NotFound();

            _context.HeroTextSection.Remove(heroText);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
