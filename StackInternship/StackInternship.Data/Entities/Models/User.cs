using System;
using System.Collections.Generic;

namespace StackInternship.Data.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] HashedPassword { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeactivatedUntil { get; set; }

        public ICollection<Resource> Resources { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Upvote> Upvotes { get; set; }
        public ICollection<Downvote> Downvotes { get; set; }
        public ICollection<ResourceView> Views { get; set; }
    }
}
