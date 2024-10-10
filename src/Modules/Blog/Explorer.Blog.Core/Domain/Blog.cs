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
        public int BlogId { get;private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        public BlogStatus Status { get; private set; }
        public List<string> ImageUrl { get; private set; }
        public DateOnly Date {  get; private set; }

        public Blog(int blogId, string title, string description, BlogStatus status, List<string> imageUrl, DateOnly date)
        {
            Validate();
            BlogId = blogId;
            Title = title;
            Description = description;
            Status = status;
            ImageUrl = imageUrl;
            Date = date;
            
        }

        private void Validate()
        {
            if (BlogId == 0) throw new ArgumentException("Invalid BlogId");
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
