using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class EmergencyEventViewModel
    {
        public int Emerg_ID { get; set; }
        public int Sec_ID { get; set; }
        public DateTime ReportDate { get; set; }
        public string Location { get; set; }
        public string ActionTaken { get; set; }
        public bool ResolutionStatus { get; set; }
        public string CauseDescription { get; set; }
        public int NumberOfInvolvedPeople { get; set; }
        public string ResolutionMethod { get; set; }

        public string Secutity_names { get; set; }
        public string involvedpeople_names{ get; set; }
        public int CampusId { get; set; }
    }
}
