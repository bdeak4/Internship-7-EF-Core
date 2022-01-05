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
        public static TRepository Create<TRepository>() where TRepository : RepositoryBase
        {
            var context = DbContextFactory.GetStackInternshipDbContext();
            var repositoryInstance = Activator
                .CreateInstance(typeof(TRepository), context) as TRepository;

            return repositoryInstance;
        }
    }
}
