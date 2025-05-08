using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class InvolvedParty
    {
        [Key]
        public int InvolvedPartyId { get; set; }
        public int PersonalId { get; set; }
        public int Emerg_ID { get; set; }
        public string PersonName { get; set; }
        public string Role { get; set; }

        public int? Sec_ID { get; set; }



        [ForeignKey("Emerg_ID")]
        public EmergencyEvent EmergencyEvent { get; set; }

        [ForeignKey("Sec_ID")]
        public SecurityStaff SecurityStaff { get; set; }
    }
}
