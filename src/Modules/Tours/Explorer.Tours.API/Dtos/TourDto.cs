using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Difficulty { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public int Status { get; set; }
        public string Tags { get; set; }
        public double Length { get; set; }
        public int IdAuthor { get; set; }
        public IEnumerable<KeyPointDto> KeyPoints { get; set; } = new List<KeyPointDto>();
        public IEnumerable<EquipmentDto> Equipments { get; set; } = new List<EquipmentDto>();
        public IEnumerable<TourDurationDto> TourDurations { get; set; } = new List<TourDurationDto>();
    }
}
