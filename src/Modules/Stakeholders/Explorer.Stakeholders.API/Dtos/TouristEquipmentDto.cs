using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class TouristEquipmentDto
    {
        public int Id { get ; set; }
        public long TouristId { get; set; }
        public int EquipmentId { get; set; }

    }
}
