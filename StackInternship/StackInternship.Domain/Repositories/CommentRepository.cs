using Microsoft.EntityFrameworkCore;
using StackInternship.Data.Entities;
using StackInternship.Data.Entities.Enums;
using StackInternship.Data.Entities.Models;
using StackInternship.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackInternship.Domain.Repositories
{
    public class CommentRepository : RepositoryBase
    {
        public CommentRepository(StackInternshipDbContext dbContext) : base(dbContext)
        {
        }



        public void Upvote(int commentId, int userId)
        {
            var upvote = new Upvote { CommentId = commentId, UserId = userId, CreatedAt = DateTime.Now };

            DbContext.Upvotes.Add(upvote);

            SaveChanges();
        }

        public void Downvote(int commentId, int userId)
        {
            var downvote = new Downvote { CommentId = commentId, UserId = userId, CreatedAt = DateTime.Now };

            DbContext.Downvotes.Add(downvote);

            SaveChanges();
        }
    }
}
