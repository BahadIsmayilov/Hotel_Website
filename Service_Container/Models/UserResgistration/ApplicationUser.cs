using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.UserResgistration
{
    public class ApplicationUser:IdentityUser
    {
        [Required,StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
