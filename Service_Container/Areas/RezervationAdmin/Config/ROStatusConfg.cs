using Microsoft.EntityFrameworkCore;
using Service_Container.Areas.RezervationAdmin.ViewModel;
using Service_Container.DAL;
using Service_Container.Models;
using Service_Container.Models.HomeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Areas.RezervationAdmin.Config
{
    public class ROStatusConfg
    {
        private readonly AppDbContext _context;

        public ROStatusConfg(AppDbContext context)
        {
            _context = context;
        }
        public void PreparePaymentsEntity(BookingDto booking)
        {
            int roomId=0;
            if (!string.IsNullOrWhiteSpace(booking.RoomType)){
                 roomId = int.Parse(booking.RoomType);
            }
            HomeRoomCategorySection category = _context.HomeRoomCategorySections.Include(x=>x.HomeRoomSections).FirstOrDefault(x => x.Id == roomId);

            HomeRoomSection room = category.HomeRoomSections.FirstOrDefault(x => x.RoomNumber == booking.RoomNumber);

            booking.RoomOrderStatus.HomeRoomSectionId = room.Id;
            booking.RoomOrderStatus.BookingId = booking.Id;

            DateTime todayDate = DateTime.Today;
            if (booking.CheckIn <= todayDate && booking.CheckOut >= todayDate)
            {
                booking.RoomOrderStatus.Status = "Occupied";
            }
            else if (booking.CheckIn > todayDate)
            {
                booking.RoomOrderStatus.Status = "Rezerved";
            }
            else
            {
                booking.RoomOrderStatus.Status = "Available";
            }
        }
    }
}
