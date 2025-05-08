using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class PermitViewModel
    {
        public int معرف_التصريح { get; set; }
        public int معرف_الزائر { get; set; }
        public int? معرف_المركبة { get; set; }  
        public DateTime تاريخ_البدء { get; set; }
        public DateTime تاريخ_الانتهاء { get; set; }
        public string نوع_التصريح { get; set; }
        public int معرف_الموظف_الأمني { get; set; }
    }
}
