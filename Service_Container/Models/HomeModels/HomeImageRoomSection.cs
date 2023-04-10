using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models
{
    public class HomeImageRoomSection
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int HomeRoomSectionId { get; set; }
        public virtual HomeRoomSection HomeRoomSection { get; set; }
    }
}
