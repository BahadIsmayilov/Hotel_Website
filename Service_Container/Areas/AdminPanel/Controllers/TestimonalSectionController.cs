using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service_Container.DAL;
using Service_Container.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using static Service_Container.Extensions.IFormFileExtensions;
using static Service_Container.Utulities.Utulitie;

namespace Service_Container.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class TestimonalSectionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _env;
        public TestimonalSectionController(AppDbContext context,IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.TestimonialSection.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            TestimonialSection testimonial = await _context.TestimonialSection.FindAsync(id);

            if (testimonial == null) return NotFound();

            return View(testimonial);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestimonialSection testimonial)
        {
            if (!ModelState.IsValid) return View(testimonial);

            if (testimonial.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo should be selected");
                return View(testimonial);
            }
            if (!testimonial.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "File type isn't valid");
                return View(testimonial);
            }
            if (testimonial.Photo.IsLessThan(2))
            {
                ModelState.AddModelError("Photo", "File size cann't more than 2 mb");
                return View(testimonial);
            }

            string fileName = await testimonial.Photo.Save(_env.WebRootPath, "testemonial");

            testimonial.Image = fileName;
            await _context.TestimonialSection.AddAsync(testimonial);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            TestimonialSection testimonial = await _context.TestimonialSection.FindAsync(id);

            if (testimonial == null) return NotFound();

            return View(testimonial);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,TestimonialSection testimonial)
        {
            if (!ModelState.IsValid) return View(testimonial);

            if (id == null) return BadRequest();

            TestimonialSection oldTestimonial = await _context.TestimonialSection.FindAsync(id);

            if (oldTestimonial == null) return NotFound();

            if (testimonial.Photo != null)
            {
                if (!testimonial.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "File type is not valid");
                    return View(testimonial);
                }
                if (testimonial.Photo.IsLessThan(2))
                {
                    ModelState.AddModelError("Photo", "File size cann't more than 2 mb");
                    return View(testimonial);
                }

                RemoveImage(_env.WebRootPath, "testemonial", oldTestimonial.Image);
                oldTestimonial.Image = await testimonial.Photo.Save(_env.WebRootPath, "testemonial");
            }
            oldTestimonial.Comment = testimonial.Comment;
            oldTestimonial.User = testimonial.User;
            oldTestimonial.Update = DateTime.Now;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            TestimonialSection testimonial = await _context.TestimonialSection.FindAsync(id);

            if (testimonial == null) return NotFound();

            return View(testimonial);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();

            TestimonialSection testimonial = await _context.TestimonialSection.FindAsync(id);

            if (testimonial == null) return NotFound();

            RemoveImage(_env.WebRootPath, "testemonial", testimonial.Image);

            _context.TestimonialSection.Remove(testimonial);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
