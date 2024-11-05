using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class Blog : Entity
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Rating> Ratings { get; set; }
        public BlogStatus Status { get; init; }
        public List<string> ImageUrl { get; set; } = new List<string>();
        public DateOnly Date {  get; init; }
        public BlogActivityStatus ActivityStatus { get; init; }
        public int OwnerId { get; init; }

        public Blog(string title, string description, BlogStatus status, DateOnly date, BlogActivityStatus activityStatus, int ownerId)
        {
            Title = title;
            Description = description;
            Status = status;
            Date = date;
            ActivityStatus = activityStatus;
            OwnerId = ownerId;
            Ratings = new List<Rating>();
            Validate();
        }

        private void Validate()
        {
            if(string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("Invalid title");
            if(string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid description");

        }
        public void UpdateRating(Rating rating) { 
            Rating r = Ratings.Find(rat => rat.UserId == rating.UserId);
            if(r!= null)
            {
                if(rating.Grade == r.Grade)
                {
                    Ratings.Remove(r);
                }
                else
                {
                    Ratings.Remove(r);
                    Ratings.Add(rating);
                }
            }
            else
            {
                Ratings.Add(rating);
            }
        }
        
    }

    public enum BlogStatus
    {
        draft,
        published,
        closed
    }
    public enum BlogActivityStatus
    {
        regular,
        active,
        famous,
        closed
    }
}
