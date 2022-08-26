using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LearningProgressItems.DTOs
{
    public class QuizDto
    {
        public int CardId { get; set; }
        public string Term { get; set; }

        public List<string> Options { get; set; }
    }
}
