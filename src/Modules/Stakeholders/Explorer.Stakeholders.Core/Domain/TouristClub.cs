using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Core.Domain
{
    public class TouristClub : Entity
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public string? Picture { get; private set; }

        public long OwnerId { get; private set; }
        public User Owner { get; private set; }

        public List<User> Members { get; private set; }

        public TouristClub(string name, string description, string? picture, long ownerId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required.");
            Description = description;
            Picture = picture;
            OwnerId = ownerId;
            Members = new List<User>();
        }
    }
}
