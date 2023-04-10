using AutoMapper;
using Service_Container.Areas.RezervationAdmin.ViewModel;
using Service_Container.Models.Rezervation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Areas.RezervationAdmin.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Booking, BookingDto>().ReverseMap();
        }
    }
}
