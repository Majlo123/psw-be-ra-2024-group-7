using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class QuizResponseDto
    {
        public long QuizId { get; set; }
        public string Message { get; set; }
        public RewardDto Reward { get; set; }
    }
}
