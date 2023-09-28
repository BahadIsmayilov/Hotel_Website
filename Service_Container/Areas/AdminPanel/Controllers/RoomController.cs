using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using Service_Container.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;
        public RoomController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.HomeRoomSections
                                .Include(x=>x.HomeImageRoomSections)
                                .Include(x=>x.HomeRoomCategorySection)
                                .ToList());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            HomeRoomSection roomSection = await _context.HomeRoomSections
                                                        .Include(x=>x.HomeRoomCategorySection)
                                                        .Include(x=>x.HomeImageRoomSections)
                                                        .FirstOrDefaultAsync(x=>x.Id==id);

            if (roomSection == null) return NotFound();

            return View(roomSection);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeRoomSection roomSection)
        {
            if (!ModelState.IsValid) return NotFound();

            await _context.HomeRoomSections.AddAsync(roomSection);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            HomeRoomSection roomSection = await _context.HomeRoomSections.FindAsync(id);

            if (roomSection == null) return NotFound();

            return View(roomSection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,HomeRoomSection roomSection)
        {
            if (!ModelState.IsValid) return View(roomSection);

            if (id == null) return NotFound();

            HomeRoomSection homeRoom = await _context.HomeRoomSections.FindAsync(id);

            if (homeRoom == null) return NotFound();

            DateTime update = DateTime.Now;

            homeRoom.BedType = roomSection.BedType;
            homeRoom.Capacity = roomSection.Capacity;
            homeRoom.Size = roomSection.Size;
            homeRoom.Cost = roomSection.Cost;
            homeRoom.RoomNumber = roomSection.RoomNumber;
            homeRoom.Service = roomSection.Service;
            homeRoom.UpdateDate = update;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            HomeRoomSection roomSection = await _context.HomeRoomSections.FindAsync(id);

            if (roomSection == null) return NotFound();

            return View(roomSection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();

            HomeRoomSection roomSection = await _context.HomeRoomSections.FindAsync(id);

            if (roomSection == null) return NotFound();

            _context.HomeRoomSections.Remove(roomSection);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
