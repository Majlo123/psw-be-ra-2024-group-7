using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{

    [JsonDerivedType(typeof(SocialEncounterDto), typeDiscriminator: "socialEncounter")]
    public class SocialEncounterDto : EncounterDto
    {
        public int PeopleNumber { get; set; }
    }
}
