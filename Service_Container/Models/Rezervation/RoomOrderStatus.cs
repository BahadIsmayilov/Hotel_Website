using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.Rezervation
{
    public class RoomOrderStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int HomeRoomSectionId { get; set;  }
        public int BookingId { get; set;  }
        public virtual HomeRoomSection HomeRoomSection { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
