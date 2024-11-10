using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{ 
    public class TourObject : Entity
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? Image { get; private set; }
        public int Category { get; private set; }
        public float Longitude { get; private set; }
        public float Latitude { get; private set; }
        

        public TourObject(string name, string description, string image, int category, float longitude, float latitude)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            Description = description;
            Image = image;
            Category = category;
            Longitude = longitude;
            Latitude = latitude;
        }

    }
}
