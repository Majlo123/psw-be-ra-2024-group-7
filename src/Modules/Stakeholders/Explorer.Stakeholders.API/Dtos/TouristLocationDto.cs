using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class TouristLocationDto
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public bool IsTourActive { get; set; }
    }
}
