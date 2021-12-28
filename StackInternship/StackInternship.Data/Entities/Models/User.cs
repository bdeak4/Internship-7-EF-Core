using System.Collections.Generic;

namespace StackInternship.Data.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Resource> Resources { get; set; }
    }
}
