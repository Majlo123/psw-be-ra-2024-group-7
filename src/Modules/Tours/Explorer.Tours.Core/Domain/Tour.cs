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
        public string Status { get; private set; }
        public string Tags { get; private set; }


        public Tour(string name, string difficulty, string description, double cost, string status, string tags)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            Difficulty = difficulty;
            Description = description;
            Cost = cost;
            Status = status;
            Tags = tags;
        }
    }
}
