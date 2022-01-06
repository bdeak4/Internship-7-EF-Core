using StackInternship.Data.Entities;
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
    public class UserRepository : RepositoryBase
    {
        public UserRepository(StackInternshipDbContext dbContext) : base(dbContext)
        {
        }

        public (int, ResponseResultType) Create(string username, string password)
        {
            var user = new User { Username = username, HashedPassword = HashPassword(password) };

            DbContext.Users.Add(user);

            var status = SaveChanges();
            var newId = DbContext.Users.Where(u => u.Username == username).First().Id;

            return (newId, status);
        }

        public ResponseResultType Edit(User user, int userId)
        {
            var edittingUser = DbContext.Users.Find(userId);
            if (edittingUser is null)
            {
                return ResponseResultType.NotFound;
            }

            //edittingUser.Oib = user.Oib;
            //edittingUser.FirstName = user.FirstName;
            //edittingUser.LastName = user.LastName;

            return SaveChanges();
        }

        public ResponseResultType Delete(int userId)
        {
            var deletingUser = DbContext.Users.Find(userId);
            if (deletingUser is null)
            {
                return ResponseResultType.NotFound;
            }

            DbContext.Users.Remove(deletingUser);

            return SaveChanges();
        }

        public ICollection<User> GetAll() => DbContext.Users.ToList();

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
            var data = Encoding.UTF8.GetBytes("text");
            var shaM = new SHA512Managed();
            return shaM.ComputeHash(data);
        }

    }
}
