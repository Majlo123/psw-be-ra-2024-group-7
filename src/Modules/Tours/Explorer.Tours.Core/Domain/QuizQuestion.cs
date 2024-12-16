using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class QuizQuestion
    {
        public int QuizId { get; set; }
        public string QuestionText { get; set; }
        public List<QuizAnswer> Answers { get; set; } = new();
        public int CorrectAnswerId { get; set; }
    }

}
