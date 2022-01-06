using Microsoft.EntityFrameworkCore;
using StackInternship.Data.Entities;
using StackInternship.Data.Entities.Enums;
using StackInternship.Data.Entities.Models;
using StackInternship.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StackInternship.Domain.Repositories
{
    public class ResourceRepository : RepositoryBase
    {
        public ResourceRepository(StackInternshipDbContext dbContext) : base(dbContext)
        {
        }

        public ICollection<Resource> GetByCategory(ResourceCategory category) =>
            DbContext.Resources
                .Include(r => r.User)
                .Include(r => r.Upvotes)
                .Include(r => r.Downvotes)
                .Include(r => r.Views)
                .Where(r => r.Category == category)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();        
    }
}
