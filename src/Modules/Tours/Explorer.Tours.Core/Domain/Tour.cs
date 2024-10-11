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
        public string Name { get; init; }
        public string Difficulty { get; init; }
        public string Description { get; init; }
        public double Cost { get; init; }
        public string Status { get; init; }
        public string Tags { get; init; }


        public Tour(string name, string difficulty, string description, double cost, string status, string tags)
        {
            Name = name;
            Difficulty = difficulty;
            Description = description;
            Cost = cost;
            Status = status;
            Tags = tags;
        }
    }
}