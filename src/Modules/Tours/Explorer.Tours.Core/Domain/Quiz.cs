using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Quiz
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public string Title { get; set; }
        public List<QuizQuestion> Questions { get; set; } = new();
    }
}
