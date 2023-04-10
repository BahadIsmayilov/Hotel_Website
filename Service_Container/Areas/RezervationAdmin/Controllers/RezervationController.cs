using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_Container.Areas.Config;
using Service_Container.Areas.RezervationAdmin.Config;
using Service_Container.Areas.RezervationAdmin.Models;
using Service_Container.Areas.RezervationAdmin.ViewModel;
using Service_Container.DAL;
using Service_Container.Models.Rezervation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Service_Container.Areas.RezervationAdmin.Controllers
{
    [Area("RezervationAdmin")]
    public class RezervationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public RezervationController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllRezervations()
        {
            var rezerved =await _context.Bookings.Include(x => x.RoomOrderStatus)
                                            .ThenInclude(x=>x.HomeRoomSection)
                                            .ThenInclude(x=>x.HomeImageRoomSections)
                                            .Include(x => x.Payments)
                                            .Where(x=>x.RoomOrderStatus.Status == "Rezerved" 
                                                  || x.RoomOrderStatus.Status == "Occupied").ToListAsync();
            DateTime todayDate = DateTime.Today;
            foreach (var item in rezerved)
            {
                if (item.CheckIn <= todayDate && item.CheckOut >= todayDate)
                {
                    item.RoomOrderStatus.Status = "Occupied";
                }
                else if (item.CheckIn > todayDate)
                {
                    item.RoomOrderStatus.Status = "Rezerved";
                }
                else
                {
                    item.RoomOrderStatus.Status = "Available";
                }
            }
            await _context.SaveChangesAsync();
            return View(rezerved);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRezervations(string customerName)
        {
            ViewData["GetCustomer"] = customerName;

            var findRezervation = await _context.Bookings.Include(x => x.RoomOrderStatus)
                                            .ThenInclude(x => x.HomeRoomSection)
                                            .ThenInclude(x => x.HomeImageRoomSections)
                                            .Include(x => x.Payments)
                                            .Where(x => (x.RoomOrderStatus.Status == "Rezerved"
                                                  || x.RoomOrderStatus.Status == "Occupied")).ToListAsync();

            if (!string.IsNullOrWhiteSpace(customerName))
            {
                findRezervation = await findRezervation.Where(x => x.CustomerName.Contains(customerName)).ToListAsync();
            }
            return View(findRezervation);
        }

        
        public  JsonResult UpdateOrderStatus(int id)
        {
            var findCustomer = _context.Bookings.Include(x => x.RoomOrderStatus).FirstOrDefault(x => x.Id == id);
            if (findCustomer != null)
            {


                findCustomer.RoomOrderStatus.Status = (findCustomer.RoomOrderStatus.Status == "Rezerved") ? "Occupied" : "Avaliable";

                _context.SaveChanges();
                return Json(new { success = true, status = findCustomer.RoomOrderStatus.Status });
            }
            else
            {
                return Json(new { success = false, message = "Room not found" });
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Category"] = _context.HomeRoomCategorySections.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingDto booking)
        {
            ViewData["Category"] = _context.HomeRoomCategorySections.ToList();

            if (!ModelState.IsValid) return View(booking);

            var existBooking = _context.Bookings.Where(x => x.RoomNumber == booking.RoomNumber).OrderByDescending(x => x.Id).FirstOrDefault();
            if (existBooking != null)
            {
                DateTime todayDate = DateTime.Today;

                if (existBooking.CheckOut >= todayDate)
                {
                    return View(booking);
                }
            }

            PaymentConfg payment = new PaymentConfg(_context);
            payment.PreparePaymentsEntity(booking);

            ROStatusConfg statusConfg = new ROStatusConfg(_context);
            statusConfg.PreparePaymentsEntity(booking);

           await _context.Bookings.AddAsync(_mapper.Map<Booking>(booking));
           await _context.SaveChangesAsync();
           return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult LoadRoomNumberByRoomCategoryId(int categoryId)
        {
            var rooms = _context.HomeRoomSections.Where(x => x.CategoryId == categoryId).Select(x=>x.RoomNumber).ToList();
            return Json(rooms);
        }
    }
}