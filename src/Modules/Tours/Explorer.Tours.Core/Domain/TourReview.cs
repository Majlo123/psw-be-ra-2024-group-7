using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain
{
    public class TourReview : Entity
    {
        public int ReviewId { get; private set; }
        public int TourId { get; private set; }
        public int Rating { get; private set; }  
        public string Comment { get; private set; } 
        public Person Tourist { get; private set; }  
        public DateTime VisitDate { get; private set; }  
        public DateTime ReviewDate { get; private set; }  
        public List<string> Images { get; private set; }


        public TourReview(int reviewId, int tourId, int rating, string comment, Person tourist, DateTime visitDate, DateTime reviewDate, List<string> images)
        {
            ReviewId = reviewId;
            TourId = tourId;
            Rating = rating;
            Comment = comment;
            Tourist = tourist;
            VisitDate = visitDate;
            ReviewDate = reviewDate;
            Images = images ?? new List<string>();

            Validate();


        }

        private void Validate()
        {
            if (TourId < 0) throw new ArgumentException("TourId must be 0 or positive number", nameof(TourId));
            if (ReviewId < 0) throw new ArgumentException("ReviewId must be 0 or positive number", nameof(ReviewId));
            if (Rating < 1 || Rating > 5) throw new ArgumentException("Rating must be between 1 and 5.");
            if (string.IsNullOrWhiteSpace(Comment)) throw new ArgumentException("Comment cannot be empty.");
            if (Tourist == null) throw new ArgumentNullException(nameof(Tourist), "Tourist information is required.");
            if (VisitDate > DateTime.Now) throw new ArgumentException("Visit date cannot be in the future.");
            if (ReviewDate > DateTime.Now) throw new ArgumentException("Review date cannot be in the future.");
            if (VisitDate > ReviewDate) throw new ArgumentException("Visit date cannot be after the review date.");
        }
    }
}
