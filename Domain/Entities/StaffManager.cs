using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StaffManager
    {
        public int StaffManagerId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public long UserId { get; set; }
        public long Password { get; set; }
        public string ContactNumber { get; set; }

        public ICollection<StaffManagerCampus> StaffManagerCampuses { get; set; }
    }
}

