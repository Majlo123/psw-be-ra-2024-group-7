using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourReviewDto
    {
        public int ReviewId { get; set; }

        public int TourId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public int Tourist { get; set; }

        public DateTime VisitDate { get; set; }

        public DateTime ReviewDate { get; set; }

        public List<string> Images { get; set; }
    }
}
