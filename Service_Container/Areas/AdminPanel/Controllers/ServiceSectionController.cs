using Microsoft.AspNetCore.Mvc;
using Service_Container.DAL;
using Service_Container.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ServiceSectionController : Controller
    {
        private readonly AppDbContext _context;
        public ServiceSectionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.ServiceSections.ToList());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            ServiceSection serviceSection = await _context.ServiceSections.FindAsync(id);

            if (serviceSection == null) return NotFound();

            return View(serviceSection);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceSection serviceSection)
        {
            if (!ModelState.IsValid) return View(serviceSection);

            await _context.ServiceSections.AddAsync(serviceSection);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            ServiceSection serviceSection = await _context.ServiceSections.FindAsync(id);

            if (serviceSection == null) return NotFound();

            return View(serviceSection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,ServiceSection serviceSection)
        {
            if (!ModelState.IsValid) return View(serviceSection);

            ServiceSection service = await _context.ServiceSections.FindAsync(id);

            DateTime update = DateTime.Now;

            service.Title = serviceSection.Title;
            service.Icon = serviceSection.Icon;
            service.Description = serviceSection.Description;
            service.UpdateDate = update;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            ServiceSection serviceSection = await _context.ServiceSections.FindAsync(id);
            if (serviceSection == null) return NotFound();

            return View(serviceSection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();

            ServiceSection serviceSection = await _context.ServiceSections.FindAsync(id);

            if (serviceSection == null) return NotFound();

            _context.ServiceSections.Remove(serviceSection);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
