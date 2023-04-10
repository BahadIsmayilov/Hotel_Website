using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.ContactModels
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Fax { get; set; }
        public string Map { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
