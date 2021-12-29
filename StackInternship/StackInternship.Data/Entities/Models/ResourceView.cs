using System;

namespace StackInternship.Data.Entities.Models
{
    public class  ResourceView
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}
