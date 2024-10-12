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

        public BlogStatus Status { get; init; }
        public List<string> ImageUrl { get; init; }
        public DateOnly Date {  get; init; }

        public Blog(string title, string description, BlogStatus status, List<string> imageUrl, DateOnly date)
        {
            Title = title;
            Description = description;
            Status = status;
            ImageUrl = imageUrl;
            Date = date;
            Validate();
        }

        private void Validate()
        {
            if(string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("Invalid title");
            if(string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid description");

        }
    }

    public enum BlogStatus
    {
        draft,
        published,
        closed
    }
}
