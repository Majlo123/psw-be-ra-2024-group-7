using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class TouristClub : Entity
    {
        public long ClubId { get; private set; }
        public string Name { get; private set; }

        public string Description { get; private set; }

        public string? Picture { get; private set; }

        public long OwnerId { get; private set; }

        public Person Owner { get; private set; }
      

        public TouristClub(string name, string description, string? picture, Person owner)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required.");
            Description = description;
            Picture = picture;
            Owner = owner;
            OwnerId = owner.UserId;
        }
    }
}
