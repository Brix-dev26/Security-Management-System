using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Visitor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitorId { get; set; }

        //id info 
        public long? NationalIdCard { get; set; }
        public string name { get; set; }
        public string E_mail { get; set; }
        public string phone_no { get; set; }

        public string nationality { get; set; }
        //passport info
        public string? passport_no { get; set; }

        public bool IsBlacklisted { get; set; } = false;

        public string? notes { get; set; }

        public ICollection<Permit> Permits { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }

}
