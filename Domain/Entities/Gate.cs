using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Gate
    {
        [Key]
        public int GateId { get; set; }
        public string gate_name  { get; set; }
        public int CampusId { get; set; }

        [ForeignKey("CampusId")]
        public Campus Campus { get; set; }
    }

}
