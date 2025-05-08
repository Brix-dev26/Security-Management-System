using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class InvolvedPartyViewModel
    {
        public int InvolvedPartyId { get; set; }
        public int PersonalId { get; set; }
        public int Emerg_ID { get; set; }
        public string PersonName { get; set; }
        public string Role { get; set; }
        public int? Sec_ID { get; set; }
    }
}
