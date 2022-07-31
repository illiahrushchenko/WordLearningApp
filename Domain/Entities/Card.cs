using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }

        public Module Module { get; set; }
        public int ModuleId { get; set; }
    }
}
