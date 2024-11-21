using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{

    [JsonDerivedType(typeof(HiddenLocationEncounterDto), typeDiscriminator: "hiddenLocationEncounter")]
    public class HiddenLocationEncounterDto : EncounterDto
    {
        public string Image { get; set; }
        public double ImageLongitude { get; set; }
        public double ImageLatitude { get; set; }
    }
}
