using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class CompletedKeyPoints : ValueObject<CompletedKeyPoints>
    {
        public int CompletedKeyPointId { get; private set; }
        public DateTime ExecutionTime { get; private set; }
        public int TouristId { get; private set; }
        public int TourId { get; private set; }

        [JsonConstructor]
        public CompletedKeyPoints(int keyPointId, DateTime executionTime, int touristId, int tourId)
        {
            CompletedKeyPointId = keyPointId;
            ExecutionTime = executionTime;
            TouristId = touristId;
            TourId = tourId;
        }

        protected override bool EqualsCore(CompletedKeyPoints completedKeyPoints)
        {
            return CompletedKeyPointId == completedKeyPoints.CompletedKeyPointId
                       && ExecutionTime == completedKeyPoints.ExecutionTime
                       && TouristId == completedKeyPoints.TouristId
                       && TourId == completedKeyPoints.TourId;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = CompletedKeyPointId.GetHashCode();
                hashCode = (hashCode * 397) ^ ExecutionTime.GetHashCode();
                hashCode = (hashCode * 397) ^ TouristId.GetHashCode();
                hashCode = (hashCode * 397) ^ TourId.GetHashCode();
                return hashCode;
            }
        }

        private void Validation(CompletedKeyPoints completedKeyPoints)
        {
            throw new NotImplementedException();
        }
    }
}
