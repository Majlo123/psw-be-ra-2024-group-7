using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class TouristClubDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string? Picture { get; set; }

        public long OwnerId { get; set; }
        public List<int> Members { get; set; } = new List<int>();
    }
}
