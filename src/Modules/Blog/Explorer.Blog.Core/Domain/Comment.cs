using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class Comment : Entity 
    {
        public int UserId { get; private set; }  
        public DateTime CreatedAt { get; private set; }  
        public string Text { get; private set; }  
        public DateTime? LastModified { get; private set; }  

        public Comment(int userId, string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Invalid Comment Text.");

            UserId = userId;
            Text = text;
            CreatedAt = DateTime.UtcNow; 
            LastModified = null;  
        }

    }
}
