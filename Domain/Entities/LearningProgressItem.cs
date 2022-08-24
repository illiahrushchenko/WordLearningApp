using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LearningProgressItem : EntityBase
    {
        public Card Card { get; set; }
        public int CardId { get; set; }

        public bool AnsweredWithQuiz { get; set; }
        public bool AnsweredWithWriting { get; set; }

        public LearningProgress LearningProgress { get; set; }
        public int? LearningProgressId { get; set; }
    }
}
