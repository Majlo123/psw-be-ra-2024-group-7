using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public BlogStatus Status { get; set; }
        public List<string> ImageUrl { get; set; }
        public DateOnly Date { get; set; }
    }
    public enum BlogStatus
    {
        draft,
        published,
        closed
    }
}
