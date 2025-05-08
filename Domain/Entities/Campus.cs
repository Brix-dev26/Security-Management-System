using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Campus
    {
        [Key]
        public int CampusId { get; set; }
        public string campus_name  { get; set; }
        public string CampusLocation { get; set; }



        public virtual ICollection<Gate> Gates { get; set; }
    }
}
