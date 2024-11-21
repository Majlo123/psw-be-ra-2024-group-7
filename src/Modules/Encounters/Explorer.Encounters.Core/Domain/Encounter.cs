using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public enum EncounterStatus
    {
        ACTIVE,
        DRAFT,
        ARCHIEVED,
        DELETED
    }

    public enum EncounterType
    {
        SOCIAL,
        HIDDENLOCATION,
        MISC
    }
    public enum TouristEncounterStatus
    {
        ACCEPTED,
        AWAITS,
        CANCELLED
    }
    public abstract class Encounter : Entity
    {
        public string Name { get; protected set; }

        public string Description { get; protected set; }
        public int Total_xp { get; protected set; }

        public int CreatorId { get; protected set; }

        public double Longitude { get; protected set; }
        public double Latitude { get; protected set; }

        public EncounterStatus Status { get; protected set; }

        public EncounterType EncounterType { get; protected set; }

        public TouristEncounterStatus? TouristRequestStatus { get; protected set; }

        public bool? isTourRequired { get; protected set; }

        public int? TourId { get; protected set; }

        public double ActivateRange { get; protected set; }


        protected Encounter() {}

        //public Encounter(string name, string description, int total_xp, int creatorId, double longitude, double latitude, EncounterStatus status, EncounterType type, TouristEncounterStatus touristEncounterStatus,bool isRequired,int tourId,double activateRange)
        //{
        //    Name = name;
        //    Description = description;
        //    Total_xp = total_xp;
        //    CreatorId = creatorId;
        //    Longitude = longitude;
        //    Latitude = latitude;
        //    Status = status;
        //    EncounterType = type;
        //    TouristRequestStatus = touristEncounterStatus;
        //    isTourRequired = isRequired;
        //    TourId = tourId;
        //    ActivateRange = activateRange;
        //}

        public abstract Encounter Clone();


    }
}
