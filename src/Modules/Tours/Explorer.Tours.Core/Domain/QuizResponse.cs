using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class QuizResponse
    {
        public long QuizId { get; set; }
        public string Message { get; set; }
        public Reward Reward { get; set; }

        public QuizResponse(long quizId, string message, Reward reward)
        {
            QuizId = quizId;
            Message = message;
            Reward = reward;
        }
    }
}
