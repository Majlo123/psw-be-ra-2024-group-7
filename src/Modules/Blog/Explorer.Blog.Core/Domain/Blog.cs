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
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public BlogStatus Status { get; set; }
        public List<string> ImageUrl { get; set; }
        public DateOnly Date {  get; set; }

        public Blog(int blogId, string title, string description, BlogStatus status, List<string> imageUrl, DateOnly date)
        {
            BlogId = blogId;
            Title = title;
            Description = description;
            Status = status;
            ImageUrl = imageUrl;
            Date = date;
            Validate();
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
