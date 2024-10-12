using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours;

namespace Explorer.Tours.API.Dtos
{
    public enum Category { WC, Restaurant, Parking, Other };
    public class ObjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Category category { get; set; }
    }
}
