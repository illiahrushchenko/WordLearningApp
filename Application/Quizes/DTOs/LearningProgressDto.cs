using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Quizes.DTOs
{
    public class LearningProgressDto
    {
        public List<LearningProgressItemDto> LearningProgressItems { get; set; }
    }

    public class LearningProgressItemDto
    {
        public bool AnsweredWithQuiz { get; set; }
        public bool AnsweredWithWriting { get; set; }
    }
}
