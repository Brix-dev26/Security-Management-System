using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class LogEntry
    {
        [Key]
        public int LogId { get; set; }

        public int VisitorId { get; set; }
        public int? VehicleId { get; set; }

        public string Visit_reason { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public int GateId { get; set; }
        public int? GateId_exit { get; set; }
        public int CampusId { get; set; } 
        public int Sec_ID { get; set; }

        public int? Sec_ID_Exit { get; set; }

        [ForeignKey("VisitorId")]
        public Visitor Visitor { get; set; }

        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }

        [ForeignKey("Sec_ID")]
        public SecurityStaff SecurityStaff { get; set; }

        [ForeignKey("GateId")]
        public Gate Gate { get; set; }

        [ForeignKey("CampusId")]
        public Campus Campus { get; set; }

    }
}
