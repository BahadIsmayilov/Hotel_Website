using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using Service_Container.Models.HomeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class RoomCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public RoomCategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.HomeRoomCategorySections.Include(x=>x.HomeRoomSections)
                                                         .ThenInclude(x=>x.HomeImageRoomSections)
                                                         .ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeRoomCategorySection categorySection)
        {
            if (!ModelState.IsValid) return NotFound();

            await _context.HomeRoomCategorySections.AddAsync(categorySection);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            HomeRoomCategorySection categorySection = await _context.HomeRoomCategorySections.FindAsync(id);

            if (categorySection == null) return NotFound();

            return View(categorySection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, HomeRoomCategorySection categorySection)
        {
            if (!ModelState.IsValid) return View(categorySection);

            HomeRoomCategorySection selectedCategorySection = await _context.HomeRoomCategorySections.FindAsync(id);

            selectedCategorySection.Name = categorySection.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            HomeRoomCategorySection categorySection = await _context.HomeRoomCategorySections.FindAsync(id);

            if (categorySection == null) return NotFound();

            return View(categorySection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();

            HomeRoomCategorySection categorySection = await _context.HomeRoomCategorySections.FindAsync(id);

            if (categorySection == null) return NotFound();

            _context.HomeRoomCategorySections.Remove(categorySection);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
