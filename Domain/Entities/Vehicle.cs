using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string Type { get; set; }
        public string Plate { get; set; }
        public int VisitorId { get; set; }

        [ForeignKey("VisitorId")]
        public Visitor Visitor { get; set; }
    }

}
