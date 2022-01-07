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

        public void Add(int userId, int resourceId, int? parentId, string content)
        {
            var comment = new Comment {
                Content = content,
                ParentId = parentId, 
                ResourceId = resourceId, 
                UserId = userId,
                CreatedAt = DateTime.Now
            };

            DbContext.Comments.Add(comment);

            SaveChanges();
        }

        public ResponseResultType Edit(int commentId, string content)
        {
            var edittingComment = DbContext.Comments.Find(commentId);
            if (edittingComment is null)
            {
                return ResponseResultType.NotFound;
            }

            edittingComment.Content = content;

            return SaveChanges();
        }

        public ResponseResultType Delete(int commentId)
        {
            var deletingComment = DbContext.Comments.Include(c => c.Children).Where(c => c.Id == commentId).First();
            if (deletingComment is null)
            {
                return ResponseResultType.NotFound;
            }

            foreach (var c in deletingComment.Children)
                Delete(c.Id);

            DbContext.Comments.Remove(deletingComment);

            return SaveChanges();
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
