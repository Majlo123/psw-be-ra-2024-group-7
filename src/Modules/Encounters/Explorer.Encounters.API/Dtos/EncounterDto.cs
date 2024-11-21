using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
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
    [JsonDerivedType(typeof(EncounterDto), typeDiscriminator: "base")]
    [JsonDerivedType(typeof(SocialEncounterDto), typeDiscriminator: "socialEncounter")]
    [JsonDerivedType(typeof(HiddenLocationEncounterDto), typeDiscriminator: "hiddenLocationEncounter")]
    [JsonDerivedType(typeof(MiscEncounterDto), typeDiscriminator: "miscEncounter")]
    public class EncounterDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalXp { get; set; }
        public int CreatorId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public EncounterStatus Status { get; set; }
        public EncounterType Type { get; set; }
        public TouristEncounterStatus? TouristEncounterStatus { get; set; }
        public bool? IsRequired { get; set; }
        public int? TourId { get; set; }
        public double ActivateRange { get; set; }

    }
}
