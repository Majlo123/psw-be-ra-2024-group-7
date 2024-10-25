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
    public class CompletedKeyPoints : ValueObject
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
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CompletedKeyPoint;
            yield return ExecutionTime;
            yield return TouristId;
            yield return TourId;
        }

 
    }
}
