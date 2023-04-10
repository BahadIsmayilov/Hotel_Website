using Service_Container.Models;
using Service_Container.Models.HomeModels;
using Service_Container.Models.Rezervation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Areas.RezervationAdmin.Models
{
    public class RezervationViewModels
    {
        public IEnumerable<HomeRoomSection> HomeRoomSections { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }
    }
}
