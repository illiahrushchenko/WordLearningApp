﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Module : EntityBase
    {
        public string Name { get; set; }
        public string Note { get; set; }

        public User Owner { get; set; }
        public int OwnerId { get; set; }

        public List<Card> Words { get; set; }
    }
}
