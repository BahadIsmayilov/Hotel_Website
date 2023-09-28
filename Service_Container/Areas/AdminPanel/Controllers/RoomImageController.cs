using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using Service_Container.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Service_Container.Extensions.IFormFileExtensions;
using static Service_Container.Utulities.Utulitie;

namespace Service_Container.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class RoomImageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _env;
        public RoomImageController(AppDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env=env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeImageRoomSections.Include(x => x.HomeRoomSection)
                                                      .ThenInclude(x => x.HomeRoomCategorySection)
                                                      .ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string categoryName)
        {
            ViewData["GetCategoryImage"] = categoryName;
            var findImage = await _context.HomeImageRoomSections.Include(x => x.HomeRoomSection)
                                                      .ThenInclude(x => x.HomeRoomCategorySection)
                                                      .ToListAsync();


            if (findImage == null) return NotFound();

            if (string.IsNullOrEmpty(categoryName)) categoryName = "";
            
            findImage = findImage.Where(x => x.HomeRoomSection
                                              .HomeRoomCategorySection.Name.ToUpper()
                                              .Contains(categoryName.ToUpper()))
                                              .ToList();

            return View(findImage);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            HomeImageRoomSection homeImage = await _context.HomeImageRoomSections
                                                         .Include(x => x.HomeRoomSection)
                                                         .ThenInclude(x => x.HomeRoomCategorySection).FirstOrDefaultAsync(x=>x.Id==id);

            if (homeImage == null) return NotFound();

            return View(homeImage);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeImageRoomSection homeImage)
        {
            if (!ModelState.IsValid) return View(homeImage);

            if(homeImage.Photo==null)
            {
                ModelState.AddModelError("Photo", "Photo should be selected");
                return View(homeImage);
            }
            if (!homeImage.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "File type is not valid");
                return View(homeImage);
            }
            if (homeImage.Photo.IsLessThan(2))
            {
                ModelState.AddModelError("Photo", "File size cann't be more than 2 mb");
                return View(homeImage);
            }

            string fileName =await homeImage.Photo.Save(_env.WebRootPath, "room");

            homeImage.Image = fileName;
            await _context.HomeImageRoomSections.AddAsync(homeImage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            HomeImageRoomSection homeImage = await _context.HomeImageRoomSections.FindAsync(id);

            if (homeImage == null) return NotFound();

            return View(homeImage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,HomeImageRoomSection homeImage)
        {
            if (!ModelState.IsValid) return View(homeImage);

            HomeImageRoomSection homeImageDb = await _context.HomeImageRoomSections.FindAsync(id);

            if (homeImage.Photo != null)
            {
                if (!homeImage.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "File type is not valid");
                    return View(homeImage);
                }
                if (homeImage.Photo.IsLessThan(2))
                {
                    ModelState.AddModelError("Photo", "File size cann't be more than 2 mb");
                    return View(homeImage);
                }

                RemoveImage(_env.WebRootPath,"room", homeImageDb.Image);

                homeImageDb.Image = await homeImage.Photo.Save(_env.WebRootPath, "room");
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            HomeImageRoomSection homeImageRoom = await _context.HomeImageRoomSections.FindAsync(id);

            if (homeImageRoom == null) return NotFound();

            return View(homeImageRoom);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {

            if (id == null) return NotFound();

            HomeImageRoomSection homeimg = await _context.HomeImageRoomSections.FindAsync(id);

            if (homeimg == null) return NotFound();

            RemoveImage(_env.WebRootPath, "room", homeimg.Image);

            _context.HomeImageRoomSections.Remove(homeimg);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
