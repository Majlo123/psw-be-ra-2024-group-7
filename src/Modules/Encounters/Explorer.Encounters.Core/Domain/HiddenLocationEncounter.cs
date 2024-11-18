using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain
{
    public class HiddenLocationEncounter : Encounter   
    {
        public string Image {  get;private set; }
        public double ImageLongitude { get; private set; }
        public double ImageLatitude { get; private set;}


        private HiddenLocationEncounter() : base() { }
        public HiddenLocationEncounter(string name, string description, int total_xp, int creatorId, double longitude, double latitude, EncounterStatus status, EncounterType type, TouristEncounterStatus touristEncounterStatus, bool isRequired, int tourId, double activateRange,string image, double imageLongitude, double imageLatitude)
            : base(name,description,total_xp,creatorId,longitude,latitude,status,type,touristEncounterStatus,isRequired,tourId,activateRange)
        {
            Image = image;
            ImageLongitude = imageLongitude;
            ImageLatitude = imageLatitude;
        }
    }
}
