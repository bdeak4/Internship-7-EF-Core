﻿using Microsoft.EntityFrameworkCore;
using StackInternship.Data.Entities;
using StackInternship.Data.Entities.Enums;
using StackInternship.Data.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace StackInternship.Domain.Repositories
{
    public class ResourceRepository : RepositoryBase
    {
        public ResourceRepository(StackInternshipDbContext dbContext) : base(dbContext)
        {
        }

        public Resource GetById(int resourceId) =>
            DbContext.Resources
                .Include(r => r.User)
                .Include(r => r.Upvotes)
                .Include(r => r.Downvotes)
                .Include(r => r.Views)
                .Include(r => r.Comments).ThenInclude(c => c.User)
                .Include(r => r.Comments).ThenInclude(c => c.Upvotes)
                .Include(r => r.Comments).ThenInclude(c => c.Downvotes)
                .Include(r => r.Comments).ThenInclude(c => c.Children)
                .Where(r => r.Id == resourceId)
                .First();

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
