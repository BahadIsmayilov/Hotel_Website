using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.Rezervation
{
    public class Payments
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public int BookingId { get; set; }
        public int HomeRoomSectionId { get; set; }
        public virtual Booking Bookings { get; set; }
        public virtual  HomeRoomSection HomeRoomSection { get; set; }
    }
}
