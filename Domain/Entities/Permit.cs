using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Permit
    {
        [Key]
        public int PermitId { get; set; }
        public int VisitorId { get; set; }
        public int? VehicleId { get; set; }  

        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string PermitType { get; set; }
        public int Sec_ID { get; set; }

        [ForeignKey("VisitorId")]
        public Visitor Visitor { get; set; }

        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }

        [ForeignKey("Sec_ID")]
        public SecurityStaff SecurityStaff { get; set; }
    }
}
