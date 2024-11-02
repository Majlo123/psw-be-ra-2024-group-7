using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.Tours.Core.Domain
{
    public enum TourStatus
    {
        Draft = 0,
        Published = 1,
        Archived = 2,
        Closed = 3
    }

    public class Tour : Entity
    {
        public string Name { get; private set; }
        public string Difficulty { get; private set; }
        public string Description { get; private set; }
        public double Cost { get; private set; }
        public TourStatus Status { get; private set; }
        public string Tags { get; private set; }
        public List<KeyPoint> KeyPoints { get; set; } = new List<KeyPoint>();
        public List<Equipment> Equipments { get; set; } = new List<Equipment>();
        public List<TourDuration> TourDurations { get; set;} = new List<TourDuration>();
        public double Length { get; private set; }
        public int AuthorId { get; private set; }
        public DateTime? PublishTime { get; private set; } = null;

        public Tour(string name, string difficulty, string description, double cost, TourStatus status, string tags, double length,int authorId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            Difficulty = difficulty;
            Description = description;
            Cost = cost;
            Status = status;
            Tags = tags;
            Length = length;
            AuthorId = authorId;
        }
        public void AddKeyPoint(KeyPoint keyPoint)
        {
            if (keyPoint == null) throw new ArgumentNullException(nameof(keyPoint));
            KeyPoints.Add(keyPoint);
        }

        public Tour Publish()
        {
            if (!CanPublish())
                throw new ArgumentException("Tura nije ispunila uslove za objavljivanje.");
            Status = TourStatus.Published;
            PublishTime = DateTime.UtcNow;
            return this;
        }

        public bool CanPublish()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(Description) &&
                   !string.IsNullOrEmpty(Tags) && KeyPoints.Count >= 2 && TourDurations.Count >= 1;
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

        public void CloseTour()
        {
            Status = TourStatus.Closed;
        }

    }
}

