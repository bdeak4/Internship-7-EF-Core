using StackInternship.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackInternship.Domain.Factories
{
    public class RepositoryFactory
    {
        static public UserRepository GetUserRepository() =>
            new UserRepository(DbContextFactory.GetStackInternshipDbContext());
    }
}
