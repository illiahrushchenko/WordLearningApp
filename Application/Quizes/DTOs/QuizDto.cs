using System.Collections.Generic;

namespace Application.Quizes.DTOs
{
    public class QuizDto
    {
        public int CardId { get; set; }
        public string Term { get; set; }

        public List<string> Options { get; set; }
    }
}
