using Service_Container.Models.Rezervation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Areas.RezervationAdmin.ViewModel
{
    public class BookingDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoomNumber { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Min length must be 3 character")]
        public string CustomerName { get; set; }
        [Required]
        [MaxLength(7, ErrorMessage = "Max length must be 7 character")]
        [MinLength(7, ErrorMessage = "Min length must be 7 character")]
        public string PinCode { get; set; }
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime? CheckOut { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Min length must be 10 number")]
        public string PhoneNumber { get; set; }
        
        public int? ExBed { get; set; }

        public bool PayFifty { get; set; }
        public bool PayAll { get; set; }
        [Required]
        public string RoomType { get; set; }
        [Required]
        public int AdultsCount { get; set; }
        public int? ChildCount { get; set; }
        public Payments Payments { get; set; } = new Payments();
        public RoomOrderStatus RoomOrderStatus { get; set; } = new RoomOrderStatus();
    }
}
