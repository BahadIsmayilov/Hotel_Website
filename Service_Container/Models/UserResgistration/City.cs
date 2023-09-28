using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.UserResgistration
{
    public class City
    {
        public City()
        {
            ApplicationUsers = new HashSet<ApplicationUser>();
        }
        public int Id { get; set; }
        [Required,StringLength(100)]
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
