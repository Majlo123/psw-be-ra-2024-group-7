using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public enum ExecutionStatus
    {
        COMPLETED,
        ABANDONED,
        ONGOING
    }
    public class TourExecution : Entity
    {
        public int TourId { get; private set; }
        public int TouristId { get; private set; }
        public DateTime TourStartDate { get; private set; }
        public DateTime TourEndDate { get; private set; }
        public DateTime LastActivity { get; private set; }
        public ExecutionStatus Status { get; private set; }
        public float CompletedPercentage { get; private set; }
        public List<Equipment> TouristEquipment { get;  set; }

        public List<CompletedKeyPoints> CompletedKeyPoints { get;  set; }
        //public ... CurrentTouristLocation { get; private set; }
        //public ... TrnasportationType { get; private set; }



    }
}

