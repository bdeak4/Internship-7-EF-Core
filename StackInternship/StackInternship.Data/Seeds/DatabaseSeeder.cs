using Microsoft.EntityFrameworkCore;
using StackInternship.Data.Entities.Enums;
using StackInternship.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StackInternship.Data.Seeds
{
    public class DatabaseSeeder
    {
        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(new List<User>
                {
                    new User
                    {
                        Id = 1,
                        Username = "ivan",
                        HashedPassword = HashPassword("password")
                    },
                    new User
                    {
                        Id = 2,
                        Username = "marko",
                        HashedPassword = HashPassword("password"),
                        IsOrganizer = true
                    },
                    new User
                    {
                        Id = 3,
                        Username = "ante",
                        HashedPassword = HashPassword("password"),
                        IsOrganizer = true
                    }
                }) ;

            builder.Entity<Resource>()
                .HasData(new List<Resource>
                {
                    new Resource
                    {
                        Id = 1,
                        Title = "post 1",
                        Content = "prvi post",
                        CreatedAt = DateTime.Today,
                        Category = ResourceCategory.Dev,
                        UserId = 1,
                    },
                    new Resource
                    {
                        Id = 2,
                        Title = "post 2",
                        Content = "drugi\n post",
                        CreatedAt = DateTime.Now,
                        Category = ResourceCategory.Dev,
                        UserId = 2,
                    },
                    new Resource
                    {
                        Id = 3,
                        Title = "dizajn radionica",
                        Content = "t\nr\ne\nc\ni\npost",
                        CreatedAt = DateTime.Now,
                        Category = ResourceCategory.Dizajn,
                        UserId = 2,
                    }
                });

            builder.Entity<Comment>()
                .HasData(new List<Comment>
                {
                    new Comment
                    {
                        Id = 1,
                        Content = "prvi",
                        CreatedAt = DateTime.Today,
                        UserId = 1,
                        ResourceId = 1,
                    },
                    new Comment
                    {
                        Id = 2,
                        Content = "drugi",
                        CreatedAt = DateTime.Now,
                        UserId = 2,
                        ResourceId = 1,
                        ParentId = 1,
                    },
                    new Comment
                    {
                        Id = 3,
                        Content = "treci",
                        CreatedAt = DateTime.Now,
                        UserId = 3,
                        ResourceId = 1,
                        ParentId = 2,
                    },
                    new Comment
                    {
                        Id = 4,
                        Content = "super post!",
                        CreatedAt = DateTime.Now,
                        UserId = 3,
                        ResourceId = 1,
                    }
                });
        }

        static private byte[] HashPassword(string password)
        {
            var data = Encoding.UTF8.GetBytes(password);
            var shaM = new SHA512Managed();
            return shaM.ComputeHash(data);
        }

    }
}
