using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using Service_Container.Models.ContactModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Service_Container.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ContactSectionController : Controller
    {
        private readonly AppDbContext _context;
        public ContactSectionController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactInfos.FirstOrDefaultAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            ContactInfo contact = await _context.ContactInfos.FindAsync(id);

            if (contact == null) return NotFound();

            return View(contact);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactInfo contact)
        {
            if (!ModelState.IsValid) return View(contact);

            await _context.ContactInfos.AddAsync(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            ContactInfo contact = await _context.ContactInfos.FindAsync(id);

            if (contact == null) return NotFound();

            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,ContactInfo contact)
        {
            if (!ModelState.IsValid) return View(contact);

            ContactInfo contactDb = await _context.ContactInfos.FindAsync(id);

            if (contactDb == null) return NotFound();

            contactDb.Header = contact.Header;
            contactDb.Text = contact.Text;
            contactDb.Address = contact.Address;
            contactDb.PhoneNumber = contact.PhoneNumber;
            contactDb.Mail = contact.Mail;
            contactDb.Fax = contact.Fax;
            contactDb.Map = contact.Map;
            contactDb.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            ContactInfo contact = await _context.ContactInfos.FindAsync(id);

            if (contact == null) return NotFound();

            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            ContactInfo contact = await _context.ContactInfos.FindAsync(id);
            if (contact == null) return NotFound();

            _context.ContactInfos.Remove(contact);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
