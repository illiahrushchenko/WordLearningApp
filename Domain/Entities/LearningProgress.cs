using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LearningProgress : EntityBase
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public Module Module { get; set; }
        public int ModuleId { get; set; }

        public List<LearningProgressItem> LearningProgressItems { get; set; }
    }
}
