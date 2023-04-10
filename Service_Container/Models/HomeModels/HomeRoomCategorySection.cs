using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.HomeModels
{
    public class HomeRoomCategorySection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<HomeRoomSection> HomeRoomSections { get; set; }
    }
}
