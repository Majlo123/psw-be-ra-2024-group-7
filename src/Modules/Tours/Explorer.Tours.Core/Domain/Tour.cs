using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Tour : Entity
    {
        public string Name { get; private set; }
        public string Difficulty { get; private set; }
        public string Description { get; private set; }
        public double Cost { get; private set; }
        public int Status { get; private set; }
        public string Tags { get; private set; }
        public List<KeyPoint> KeyPoints { get; set; } = new List<KeyPoint>();
        public List<Equipment> Equipments { get; set; } = new List<Equipment>();
        public List<TourDuration> TourDurations { get; set;} = new List<TourDuration>();
        public double Length { get; private set; }

        public Tour(string name, string difficulty, string description, double cost, int status, string tags, double length)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            Difficulty = difficulty;
            Description = description;
            Cost = cost;
            Status = status;
            Tags = tags;
            Length = length;
        }
        public void AddKeyPoint(KeyPoint keyPoint)
        {
            if (keyPoint == null) throw new ArgumentNullException(nameof(keyPoint));
            KeyPoints.Add(keyPoint);
        }

        public bool Publish()
        {
            throw new NotImplementedException();
        }

        public bool Archive()
        {
            throw new NotImplementedException();
        }

        public bool Valid()
        {
            throw new NotImplementedException();
        }
        
        public void IncrementDuration(TourDuration tourDruation)
        {
            throw new NotImplementedException();
        }

        public void IncrementLength(double length)
        {
            throw new NotSupportedException();
        }

    }
}

