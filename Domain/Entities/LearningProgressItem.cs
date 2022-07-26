﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LearningProgressItem
    {
        public int Id { get; set; }

        public ModuleItem ModuleItem { get; set; }
        public int ModuleItemId { get; set; }

        public LearningProgress LearningProgress { get; set; }
        public int LearningProgressId { get; set; }
    }
}
