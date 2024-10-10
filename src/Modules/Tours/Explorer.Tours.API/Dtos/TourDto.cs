using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourDto
    {
        public string Name { get; set; }
        public string Difficulty { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string Status { get; set; }
        public string Tags { get; set; }

    }
}
