using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
   public class LogEntryViewModel
    {
        public int LogId { get; set; }

        public int VisitorId { get; set; }
        public int? VehicleId { get; set; }
        public int Sec_ID { get; set; }
        public string Visit_reason { get; set; }


        public int GateId { get; set; }
        public int CampusId { get; set; }

        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }

        public string GateName { get; set; }
        public string CampusName { get; set; }

        public int? Sec_ID_Exit { get; set; }


        public int? GateId_exit { get; set; }
        public string? ExitGateName { get; set; }
        public string SecExitName { get; set; }

        public string SecEntanceName {  get; set; }

        public string? VisitorName { get; set; }
        public string? VisitorPhone { get; set; }
        public long? VisitorNationalId { get; set; }
        public string VisitorPassport { get; set; }  

    }
}
