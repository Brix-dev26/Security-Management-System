using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StaffManagerCampus
    {
        public int StaffManagerId { get; set; }
        public StaffManager StaffManager { get; set; }

        public int CampusId { get; set; }
        public Campus Campus { get; set; }
    }
}