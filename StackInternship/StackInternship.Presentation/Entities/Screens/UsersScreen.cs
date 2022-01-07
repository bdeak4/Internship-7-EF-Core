using StackInternship.Domain.Enums;
using StackInternship.Domain.Factories;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Linq;

namespace StackInternship.Presentation.Entities.Screens
{
    public class UsersScreen : IScreen
    {
        public int UserId;
        public UserFilter Filter = UserFilter.NoFilter;

        public IScreen Render()
        {
            var userRepository = RepositoryFactory.CreateUserRepository();

            Console.Clear();

            var users = userRepository.GetAll().Where(u =>
            {
                switch (Filter)
                {
                    case UserFilter.Organizers:
                        return userRepository.IsOrganizator(u.Id);

                    case UserFilter.Interns:
                        return !userRepository.IsOrganizator(u.Id);

                    case UserFilter.TrustedUsers:
                        return userRepository.IsTrusted(u.Id);
                }
                return true;
            }).ToList();

            Console.WriteLine($@"Korisnici {(Filter != UserFilter.NoFilter ? $"(primjenjen filter: {Filter})" : "")}
{string.Join("\n", users.Select((u, i) => $"{i + 1} - {Helpers.PrintUsername(u, 0)}").ToList())}

Filteri:
{users.Count + 1} - Prikazi samo organizatore
{users.Count + 2} - Prikazi samo interne
{users.Count + 3} - Prikazi samo trusted korisnike
{users.Count + 4} - Prikazi sve
{users.Count + 5} - Povratak u dashboard
q - Quit");

            var input = Helpers.NumberInput(max: users.Count + 5);

            if (input == null)
                return null;

            if (input == (users.Count + 1))
                return new UsersScreen { UserId = UserId, Filter = UserFilter.Organizers };

            if (input == (users.Count + 2))
                return new UsersScreen { UserId = UserId, Filter = UserFilter.Interns };

            if (input == (users.Count + 3))
                return new UsersScreen { UserId = UserId, Filter = UserFilter.TrustedUsers };

            if (input == (users.Count + 4))
                return new UsersScreen { UserId = UserId };

            if (input == (users.Count + 5))
                return new DashboardScreen { UserId = UserId };

            return new UserProfileScreen
            {
                UserId = UserId,
                ProfileUserId = users[input.GetValueOrDefault() - 1].Id
            };
        }
    }
}
