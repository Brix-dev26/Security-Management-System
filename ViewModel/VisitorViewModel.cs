using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class VisitorViewModel
    {
        public int VisitorId { get; set; }

        //id info 
        public long? NationalIdCard { get; set; }
        public string name { get; set; }
        public string E_mail { get; set; }
        public string phone_no { get; set; }

        public string nationality { get; set; }
        //passport info
        public string? passport_no { get; set; }

        public bool? IsBlacklisted { get; set; }

        public string notes {  get; set; }

    }
}
