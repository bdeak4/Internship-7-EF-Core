using Microsoft.EntityFrameworkCore;
using StackInternship.Data.Entities;
using System.Configuration;

namespace StackInternship.Domain.Factories
{
    public static class DbContextFactory
    {
        public static StackInternshipDbContext GetStackInternshipDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(ConfigurationManager.ConnectionStrings["StackInternship"].ConnectionString)
                .Options;

            return new StackInternshipDbContext(options);
        }
    }
}
