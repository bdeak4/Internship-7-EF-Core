using StackInternship.Domain.Repositories;

namespace StackInternship.Domain.Factories
{
    public class RepositoryFactory
    {
        static public UserRepository CreateUserRepository() =>
            new UserRepository(DbContextFactory.GetStackInternshipDbContext());

        static public ResourceRepository CreateResourceRepository() =>
            new ResourceRepository(DbContextFactory.GetStackInternshipDbContext());

        static public CommentRepository CreateCommentRepository() =>
            new CommentRepository(DbContextFactory.GetStackInternshipDbContext());
    }
}
