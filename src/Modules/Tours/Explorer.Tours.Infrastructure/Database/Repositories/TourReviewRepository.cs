using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    internal class TourReviewRepository : ITourReviewRepository
    {
        private readonly ToursContext _context;

        public TourReviewRepository(ToursContext context)
        {
            _context = context;
        }

        public TourReview Get(int reviewId)
        {
            return _context.TourReview.FirstOrDefault(tr => tr.Id == reviewId);
        }

        public List<TourReview> GetReviewsForTour(int tourId)
        {
            return  _context.TourReview
                                 .Where(tr => tr.TourId == tourId)
                                 .ToList();
        }

        
    }
}
