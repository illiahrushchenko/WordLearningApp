using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LearningProgresses.DTOs
{
    public class LearningProgressDto
    {
        public List<LearningProgressItemDto> LearningProgressItems { get; set; }

        public int QuizAnswersCount =>
            LearningProgressItems.Where(x => x.AnsweredWithQuiz).Count();
        public int WritingAnswersCount =>
            LearningProgressItems.Where(x => x.AnsweredWithWriting).Count();
    }

    public class LearningProgressItemDto
    {
        public bool AnsweredWithQuiz { get; set; }
        public bool AnsweredWithWriting { get; set; }
    }
}
