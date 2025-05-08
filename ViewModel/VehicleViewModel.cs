using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class VehicleViewModel
    {
        public int? VehicleId { get; set; }
        public string Type { get; set; }
        public string Plate { get; set; }
        public int VisitorId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
