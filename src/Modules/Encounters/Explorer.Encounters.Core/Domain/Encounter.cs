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
        LOCATION,
        MISC
    }
    public enum TouristEncounterStatus
    {
        ACCEPTED,
        AWAITS,
        CANCELLED
    }
    public class Encounter : Entity
    {
        public string Name { get; private set; }

        public string Description { get; private set; }
        public int Total_xp { get; private set; }

        public int CreatorId { get; private set; }

        public double longitude { get; private set; }
        public double latitude { get; private set; }

        public EncounterStatus Status { get; private set;}

        public EncounterType Type { get; private set; }

        public TouristEncounterStatus  TouristStatus { get; private set; }

        public bool isTourRequired { get; private set; }

        public int tourId { get; private set; }

        // Da li 3 ili 1 VO??




    }
}
