using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class SecurityStaff
    {
        [Key]
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

        public ICollection<EmergencyEvent> EmergencyEvents { get; set; }
        public ICollection<Permit> PermitsGranted { get; set; }
        public ICollection<InvolvedParty> InvolvedParties { get; set; }

        [ForeignKey("GateId")]
        public Gate Gate { get; set; }

        [ForeignKey("CampusId")]
        public Campus Campus { get; set; }
    }

 
    
}
