using Service_Container.Models.HomeModels;
using Service_Container.Models.Rezervation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models
{
    public class HomeRoomSection
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int Size { get; set; }
        public int Capacity { get; set; }
        public string Service { get; set; }
        public string BedType { get; set; }
        public string RoomNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<HomeImageRoomSection> HomeImageRoomSections { get; set; }
        public virtual Payments Payments { get; set; }
        public virtual HomeRoomCategorySection HomeRoomCategorySection { get; set; }
        public virtual RoomOrderStatus RoomOrderStatus { get; set; }

    }
}
