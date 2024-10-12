using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class TouristEquipment : Entity
    {
        public long TouristId { get; init; }
        public int EquipmentId { get; init; }
        public TouristEquipment(long touristId, int equipmentId)
        {
            TouristId = touristId;
            EquipmentId = equipmentId;
            Validate();
        }
        public void Validate()
        {
            if (EquipmentId <= 0)
                throw new ArgumentException("TourId must be greater than 0", nameof(EquipmentId));
            if(TouristId<= 0)
                throw new ArgumentException("TourId must be greater than 0", nameof(TouristId));

        }
    }
}
