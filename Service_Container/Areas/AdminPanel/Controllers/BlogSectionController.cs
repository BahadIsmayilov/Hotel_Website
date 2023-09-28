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
    public class BlogSectionController : Controller
    {
        private readonly AppDbContext _context;

        public BlogSectionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.BlogsSections.Include(x=>x.BlogsImageSections).ToList());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            BlogsSection blog = await _context.BlogsSections.Include(x=>x.BlogsImageSections)
                                                            .FirstOrDefaultAsync(x=>x.Id==id);

            if (blog == null) return NotFound();

            return View(blog);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogsSection blog)
        {
            if (!ModelState.IsValid) return View(blog);

            await _context.BlogsSections.AddAsync(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            BlogsSection blog = await _context.BlogsSections.FindAsync(id);

            if (blog == null) return NotFound();

            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,BlogsSection blog)
        {
            if (!ModelState.IsValid) return View(blog);

            if (id == null) return NotFound();

            BlogsSection dbBlog = await _context.BlogsSections.FindAsync(id);


            dbBlog.Title = blog.Title;
            dbBlog.TravelPlace = blog.TravelPlace;
            dbBlog.TravelDate = blog.TravelDate;
            dbBlog.UpdaetDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            BlogsSection blog = await _context.BlogsSections.FindAsync(id);

            if (blog == null) return NotFound();

            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();

            BlogsSection blog = await _context.BlogsSections.FindAsync(id);

            if (blog == null) return NotFound();

            _context.BlogsSections.Remove(blog);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
