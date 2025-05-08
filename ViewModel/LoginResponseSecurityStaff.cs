using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class LoginResponseSecurityStaff
    {
        public string Token { get; set; }
        public SecurityStaffViewModel Staff { get; set; }
    }
}
