using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.DAL;
using Service_Container.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service_Container.SDUtilities;
namespace Service_Container.Controllers
{
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;
        public NewsController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles=SD.AdminRole+", "+SD.ModeratorRole+ ", "+SD.MemberRole)]
        public IActionResult Index()
        {
            ViewBag.TotalCount = _context.BlogsSections.Count();
            return View(_context.BlogsSections.Include(x => x.BlogsImageSections)
                                              .OrderByDescending(x => x.Id)
                                              .Take(6));
        }
        public IActionResult LoadImages(int skip)
        {
            #region ReturnJson
            //return Json(new
            //{
            //   data= _context.BlogsSections.Include(x => x.BlogsImageSections).OrderByDescending(x => x.Id).Select(x=>new { 
            //     x.Id,
            //     x.Title,
            //     x.TravelPlace,
            //     x.TravelDate,
            //     Images= x.BlogsImageSections.Select(xp => xp.Image).FirstOrDefault()
            //   })
            //});
            #endregion
            var model = _context.BlogsSections.Include(x => x.BlogsImageSections)
                                              .OrderByDescending(x => x.Id)
                                              .Skip(skip)
                                              .Take(3);

            return PartialView("_BlogsPartial", model);

        }
    }
}
