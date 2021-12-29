using System;
using System.Collections.Generic;

namespace StackInternship.Data.Entities.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? ParentId { get; set; }
        public Comment Parent { get; set; }
        public ICollection<Comment> Children { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ResourceId { get; set; }
        public Resource Resource { get; set; }

        public ICollection<Upvote> Upvotes { get; set; }
        public ICollection<Downvote> Downvotes { get; set; }
    }
}
