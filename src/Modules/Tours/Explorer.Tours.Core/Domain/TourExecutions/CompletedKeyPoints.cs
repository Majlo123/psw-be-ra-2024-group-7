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
        public KeyPoint CompletedKeyPoint { get; private set; }
        public DateTime ExecutionTime { get; private set; }
        public int TouristId { get; private set; }
        public int TourId { get; private set; }

        [JsonConstructor]
        public CompletedKeyPoints(KeyPoint keyPoint, DateTime executionTime, int touristId, int tourId)
        {
            CompletedKeyPoint = keyPoint;
            ExecutionTime = executionTime;
            TouristId = touristId;
            TourId = tourId;
        }

        protected override bool EqualsCore(CompletedKeyPoints completedKeyPoints)
        {
            return CompletedKeyPoint == completedKeyPoints.CompletedKeyPoint
                       && ExecutionTime == completedKeyPoints.ExecutionTime
                       && TouristId == completedKeyPoints.TouristId
                       && TourId == completedKeyPoints.TourId;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = CompletedKeyPoint.GetHashCode();
                hashCode = (hashCode * 357) ^ ExecutionTime.GetHashCode();
                hashCode = (hashCode * 357) ^ TouristId.GetHashCode();
                hashCode = (hashCode * 357) ^ TourId.GetHashCode();
                return hashCode;
            }
        }
    }
}
