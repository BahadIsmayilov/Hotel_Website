using Microsoft.EntityFrameworkCore;
using Service_Container.Areas.RezervationAdmin.ViewModel;
using Service_Container.DAL;
using Service_Container.Models;
using Service_Container.Models.HomeModels;
using Service_Container.Models.Rezervation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Areas.Config
{
    public class PaymentConfg
    {
        private readonly AppDbContext _context;

        public PaymentConfg(AppDbContext context)
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

            HomeRoomSection room = category.HomeRoomSections.Where(x => x.RoomNumber == booking.RoomNumber).FirstOrDefault();

            booking.Payments.HomeRoomSectionId = room.Id;
            booking.Payments.BookingId = booking.Id;

            int price = room.Cost;
            int totalprice = 0;

            int extraBedCount = (int)(booking.ExBed);
            if (extraBedCount == 0) totalprice = price;
            else if (extraBedCount == 1) totalprice = price + 30;
            else totalprice = price + 60;

            booking.Payments.TotalPrice = totalprice;

            if (booking.PayFifty)
            {
                int paidAmount = (totalprice * 50) / 100;
                booking.Payments.Amount = paidAmount;
                booking.Payments.PaymentStatus = "pay 50";
            }
            else
            {
                booking.Payments.Amount = totalprice;
                booking.Payments.PaymentStatus = "pay 100";
            }
        }
    }
}
