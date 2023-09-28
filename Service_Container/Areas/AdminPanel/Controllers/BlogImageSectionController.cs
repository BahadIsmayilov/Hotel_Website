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
    public class BlogImageSectionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _env;
        public BlogImageSectionController(AppDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogsImageSections.Include(x=>x.BlogsSection).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string title)
        {
            ViewData["GetTitle"] = title;

            var images = await _context.BlogsImageSections.Include(x => x.BlogsSection).ToListAsync();

            if (images == null) return NotFound();

            if (string.IsNullOrEmpty(title))
                title = "";

            images =images.Where(x => x.BlogsSection.Title.ToUpper().Contains(title.ToUpper())).ToList();

            return View(images);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            BlogsImageSection blogImage = await _context.BlogsImageSections
                                         .Include(x => x.BlogsSection)
                                         .FirstOrDefaultAsync(x => x.Id == id);

            if (blogImage == null) return NotFound();

            return View(blogImage);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogsImageSection blogsImage)
        {
            if (!ModelState.IsValid) return View(blogsImage);

            if (blogsImage.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo should be selected");
                return View(blogsImage);
            }
            if (!blogsImage.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "File type is not valid");
                return View(blogsImage);
            }
            if (blogsImage.Photo.IsLessThan(2))
            {
                ModelState.AddModelError("Photo", "Photo size cann't be more than 2 mb");
                return View(blogsImage);
            }

            string fileName = await blogsImage.Photo.Save(_env.WebRootPath,"blog");

            blogsImage.Image = fileName;
            await _context.BlogsImageSections.AddAsync(blogsImage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            BlogsImageSection blogsImage = await _context.BlogsImageSections.FindAsync(id);

            if (blogsImage == null) return NotFound();

            return View(blogsImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,BlogsImageSection blogsImage)
        {
            if (!ModelState.IsValid) return NotFound();

            if (id == null) return NotFound();

            BlogsImageSection images = await _context.BlogsImageSections.FindAsync(id);

            if (images == null) return NotFound();


            if (blogsImage.Photo != null)
            {
                if (blogsImage.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "File type is not valid");
                    return View(blogsImage);
                }
                if (blogsImage.Photo.IsLessThan(2))
                {
                    ModelState.AddModelError("Photo", "File size cann't more than 2 mb");
                    return View(blogsImage);
                }

                RemoveImage(_env.WebRootPath, "blog", images.Image);
                images.Image = await blogsImage.Photo.Save(_env.WebRootPath, "blog");
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            BlogsImageSection blogsImage = await _context.BlogsImageSections.FindAsync(id);

            if (blogsImage == null) return NotFound();

            return View(blogsImage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();

            BlogsImageSection blogsImage = await _context.BlogsImageSections.FindAsync(id);

            if (blogsImage == null) return NotFound();

            RemoveImage(_env.WebRootPath, "blog", blogsImage.Image);

            _context.BlogsImageSections.Remove(blogsImage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
