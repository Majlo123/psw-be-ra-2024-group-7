using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{ 
    public enum Category { WC, Restaurant, Parking, Other};
    public class Object : Entity
    {
        public int Id { get; init; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? Image { get; private set; }
        public Category Category { get; private set; }

        public Object(string name, string description, string image, Category category)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            Description = description;
            Image = image;
            Category = category;
        }

    }
}
