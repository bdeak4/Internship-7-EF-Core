using StackInternship.Data.Entities.Enums;
using System;

namespace StackInternship.Data.Entities.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ResourceCategory Category { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
