using StackInternship.Data.Entities;
using StackInternship.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackInternship.Domain.Repositories
{

    public abstract class RepositoryBase
    {
        protected readonly StackInternshipDbContext DbContext;

        protected RepositoryBase(StackInternshipDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected ResponseResultType SaveChanges()
        {
            var hasChanges = DbContext.SaveChanges() > 0;
            if (!hasChanges)
            {
                return ResponseResultType.NoChanges;
            }

            return ResponseResultType.Success;
        }
    }
}
