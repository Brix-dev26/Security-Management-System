using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class SecurityStaffViewModel
    {
        public int Sec_ID { get; set; }
        public long NationalIdCard { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string ShiftTime { get; set; }
        public string ContactNumber { get; set; }
        public long UserId { get; set; }
        public long Password { get; set; }

        public int GateId { get; set; }
        public int CampusId { get; set; }


    }
}
