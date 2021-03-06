using Microsoft.EntityFrameworkCore;
using StackInternship.Data.Entities;
using StackInternship.Data.Entities.Models;
using StackInternship.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace StackInternship.Domain.Repositories
{
    public class UserRepository : RepositoryBase
    {
        public UserRepository(StackInternshipDbContext dbContext) : base(dbContext)
        {
        }

        public (int, ResponseResultType) Create(string username, string password)
        {
            var user = new User { Username = username, HashedPassword = HashPassword(password), CreatedAt = DateTime.Now };

            DbContext.Users.Add(user);

            var status = SaveChanges();
            var newId = DbContext.Users.Where(u => u.Username == username).First().Id;

            return (newId, status);
        }

        public ResponseResultType Edit(int userId, string username)
        {
            var edittingUser = DbContext.Users.Find(userId);
            if (edittingUser is null)
            {
                return ResponseResultType.NotFound;
            }

            edittingUser.Username = username;

            return SaveChanges();
        }

        public ResponseResultType Deactivate(int userId, int days)
        {
            var deactivatingUser = DbContext.Users.Find(userId);
            if (deactivatingUser is null)
            {
                return ResponseResultType.NotFound;
            }

            deactivatingUser.DeactivatedUntil = DateTime.Now.AddDays(days);

            return SaveChanges();
        }

        public ResponseResultType UnDeactivate(int userId)
        {
            var deactivatingUser = DbContext.Users.Find(userId);
            if (deactivatingUser is null)
            {
                return ResponseResultType.NotFound;
            }

            deactivatingUser.DeactivatedUntil = null;

            return SaveChanges();
        }

        public bool CheckDeactivation(int userId) =>
            DbContext.Users.Where(u => u.Id == userId && u.DeactivatedUntil > DateTime.Now).Any();

        public ICollection<User> GetAll() => DbContext.Users.ToList();

        public User GetById(int userId) =>
            DbContext.Users
                .Include(r => r.Resources)
                .Include(r => r.Comments)
                .Include(r => r.Upvotes)
                .Include(r => r.Downvotes)
                .Include(r => r.Views)
                .Where(u => u.Id == userId)
                .FirstOrDefault();

        public int GetReceivedUpvotesCount(int userId) =>
            DbContext.Upvotes
                .Include(u => u.Comment)
                .Where(u => u.Comment.UserId == userId)
                .Count() +
            DbContext.Upvotes
                .Include(u => u.Resource)
                .Where(u => u.Resource.UserId == userId)
                .Count();

        public int GetReceivedDownvotesCount(int userId) =>
            DbContext.Downvotes
                .Include(d => d.Comment)
                .Where(d => d.Comment.UserId == userId)
                .Count() +
            DbContext.Downvotes
                .Include(d => d.Resource)
                .Where(d => d.Resource.UserId == userId)
                .Count();

        public bool Exists(string username) =>
            DbContext.Users.Where(u => u.Username == username).Any();

        public bool CheckPassword(string username, string password) =>
            DbContext.Users.Where(
                u => u.Username == username && u.HashedPassword == HashPassword(password)
            ).Any();

        public int GetIdByUsername(string username) =>
            DbContext.Users.Where(u => u.Username == username).FirstOrDefault().Id;

        private byte[] HashPassword(string password)
        {
            var data = Encoding.UTF8.GetBytes(password);
            var shaM = new SHA512Managed();
            return shaM.ComputeHash(data);
        }

        public int CalculateRep(int userId)
        {
            var rep = DbContext.Upvotes
                .Include(u => u.Comment)
                .Where(u => u.UserId == userId && u.CommentId != null)
                .Select(u => u.Comment.ParentId == null ? 10 : 5)
                .Sum();

            rep += DbContext.Upvotes
                .Include(u => u.Comment)
                .Include(u => u.Comment.Upvotes)
                .Include(u => u.Comment.Children)
                .Where(u => u.CommentId != null && u.Comment.UserId == userId)
                .Where(u => u.Comment.Upvotes.Any() || u.Comment.Children.Any())
                .Count() * 15;

            rep -= DbContext.Downvotes
                .Include(d => d.Comment)
                .Where(d => d.UserId == userId && d.CommentId != null)
                .Count();

            rep -= DbContext.Downvotes
                .Include(d => d.Comment)
                .Include(d => d.Comment.Downvotes)
                .Include(d => d.Comment.Children)
                .Where(d => d.CommentId != null && d.Comment.UserId == userId)
                .Select(d => d.Comment.Downvotes.Count * (d.Comment.Children.Any() ? 2 : 3))
                .ToList()
                .Sum();

            if (rep < 1)
                return 1;

            return rep;
        }

        public bool IsOrganizator(int userId) =>
            DbContext.Users.Where(u => u.Id == userId && u.IsOrganizer).Any() || CalculateRep(userId) >= 100000;

        public bool IsTrusted(int userId) => CalculateRep(userId) >= 1000 && !IsOrganizator(userId);

        public bool CanUpvoteResource(int userId, int resourceId) =>
            !OwnsResource(userId, resourceId) &&
            !DbContext.Upvotes.Where(u => u.UserId == userId && u.ResourceId == resourceId).Any() &&
            !DbContext.Downvotes.Where(d => d.UserId == userId && d.ResourceId == resourceId).Any();

        public bool CanDownvoteResource(int userId, int resourceId) =>
            !OwnsResource(userId, resourceId) &&
            !DbContext.Upvotes.Where(u => u.UserId == userId && u.ResourceId == resourceId).Any() &&
            !DbContext.Downvotes.Where(d => d.UserId == userId && d.ResourceId == resourceId).Any();

        private bool OwnsResource(int userId, int resourceId) =>
            DbContext.Resources.Where(r => r.Id == resourceId && r.UserId == userId).Any();

        public bool CanCreateComment(int userId) =>
            IsOrganizator(userId) || CalculateRep(userId) >= 1;

        public bool CanCreateSubComment(int userId) =>
            IsOrganizator(userId) || CalculateRep(userId) >= 3;

        public bool CanUpvoteComment(int userId, int commentId) =>
            !OwnsComment(userId, commentId) && (
                IsOrganizator(userId) ||
                CalculateRep(userId) >= 5
            ) &&
            !DbContext.Upvotes.Where(u => u.UserId == userId && u.CommentId == commentId).Any() &&
            !DbContext.Downvotes.Where(d => d.UserId == userId && d.CommentId == commentId).Any();


        public bool CanDownvoteComment(int userId, int commentId) =>
            !OwnsComment(userId, commentId) && (
                IsOrganizator(userId) ||
                CalculateRep(userId) >= (DbContext.Comments.Where(c => c.Id == commentId && c.ParentId == null).Any() ? 20 : 15)
            ) &&
            !DbContext.Upvotes.Where(u => u.UserId == userId && u.CommentId == commentId).Any() &&
            !DbContext.Downvotes.Where(d => d.UserId == userId && d.CommentId == commentId).Any();

        public bool CanEditComment(int userId, int commentId) =>
            IsOrganizator(userId) ||
            CalculateRep(userId) >= (OwnsComment(userId, commentId) ? 100 : 250);

        public bool CanDeleteComment(int userId) =>
            IsOrganizator(userId) ||
            CalculateRep(userId) >= 500;

        private bool OwnsComment(int userId, int commentId) =>
            DbContext.Comments.Where(c => c.Id == commentId && c.UserId == userId).Any();
    }
}
