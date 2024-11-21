using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class MiscEncounter : Encounter
    {
        public string Instructions { get; private set; }

        public override Encounter Clone()
        {
            return new MiscEncounter
            {
                Instructions = Instructions,
                Name = Name,
                Description = Description,
                Total_xp = Total_xp,
                CreatorId = CreatorId,
                Longitude = Longitude,
                Latitude = Latitude,
                Status = Status,
                EncounterType = EncounterType,
                TouristRequestStatus = TouristRequestStatus,
                isTourRequired = isTourRequired,
                TourId = TourId,
                ActivateRange = ActivateRange
            };
        }

        //private MiscEncounter() : base() { }
        //public MiscEncounter(string name, string description, int total_xp, int creatorId, double longitude, double latitude, EncounterStatus status, EncounterType type, TouristEncounterStatus touristEncounterStatus, bool isRequired, int tourId, double activateRange, string instructions)
        //: base(name, description, total_xp, creatorId, longitude, latitude, status, type, touristEncounterStatus, isRequired, tourId, activateRange)
        //{
        //    Instructions = instructions;
        //}
    }
}
