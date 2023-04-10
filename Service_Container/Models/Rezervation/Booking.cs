using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.Rezervation
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string PinCode { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string PhoneNumber { get; set; }
        public string RoomNumber { get; set; }
        public int? ExBed { get; set; }
        public bool? PayFifty { get; set; }
        public bool? PayAll { get; set; }
        public string Roomtype { get; set; }
        public int AdultsCount { get; set; }
        public int? ChildCount { get; set; }
        public virtual Payments Payments { get; set; }
        public virtual RoomOrderStatus RoomOrderStatus { get; set; }

    }
}
