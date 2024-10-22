using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain;

public class TourEquipment : BuildingBlocks.Core.Domain.Entity
{
    public int TourId { get; init; }
    public int EquipmentId { get; init;}

    public TourEquipment(int tourId, int equipmentId)
    {
        //if (int.IsNegative(tourId)) throw new ArgumentException("Invalid TourId.");
        //if (int.IsNegative(equipmentId)) throw new ArgumentException("Invalid EquipmentId.");
        TourId = tourId; 
        EquipmentId = equipmentId;
    }
}
