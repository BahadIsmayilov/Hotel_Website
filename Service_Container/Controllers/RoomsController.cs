using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
namespace Service_Container.Controllers
{
    public class RoomsController : Controller
    {
        private readonly AppDbContext _context;
        public RoomsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.TotalCount = _context.HomeRoomSections.Count();
            return View(_context.HomeRoomSections.Include(x=>x.HomeImageRoomSections).OrderByDescending(x=>x.Id).ToPagedList(page,6));
        }
        public IActionResult NextImages()
        {
            var model = _context.HomeRoomSections.Include(x => x.HomeImageRoomSections).OrderByDescending(x => x.Id).Take(6);
            return PartialView("_RoomsPartial", model);
        }
    }
}
