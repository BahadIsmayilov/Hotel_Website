using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Models.AboutModels
{
    public class AboutTextSection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AboutCompanyActivity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
