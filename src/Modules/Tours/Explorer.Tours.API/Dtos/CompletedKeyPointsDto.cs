using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CompletedKeyPointsDto
    {
        public int CompletedKeyPointId { get;  set; }
        public DateTime ExecutionTime { get;  set; }
        public int TouristId { get;  set; }
        public int TourId { get;  set; }

    }
}
