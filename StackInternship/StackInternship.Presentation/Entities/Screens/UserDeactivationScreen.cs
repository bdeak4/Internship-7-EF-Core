using StackInternship.Domain.Enums;
using StackInternship.Domain.Factories;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Linq;

namespace StackInternship.Presentation.Entities.Screens
{
    public class UserDeactivationScreen : IScreen
    {
        public int UserId;

        public IScreen Render()
        {
            var userRepository = RepositoryFactory.CreateUserRepository();

            Console.Clear();

            var users = userRepository.GetAll().Where(u =>
                !userRepository.IsOrganizator(u.Id) &&
                (u.DeactivatedUntil == null || u.DeactivatedUntil < DateTime.Now)
                ).ToList();

            Console.WriteLine($@"Interni koji nestrpljivo cekaju da im se deaktiviraju racuni
{string.Join("\n", users.Select((u, i) => $"{i + 1} - {Helpers.PrintUsername(u, 0)}").ToList())}
{users.Count + 1} - Povratak u dashboard
q - Quit");

            var input = Helpers.NumberInput(max: users.Count + 1);

            if (input == null)
                return null;

            if (input == (users.Count + 1))
                return new DashboardScreen { UserId = UserId };

            Console.WriteLine("Unesite broj dana koliko ce racun biti deaktiviran");
            var days = Helpers.NumberInput(max: 30);

            var status = userRepository.Deactivate(input.GetValueOrDefault(), days.GetValueOrDefault());

            if (status == ResponseResultType.Success)
                return new UserDeactivatedScreen { UserId = UserId };

            return new ErrorScreen { Status = status };
        }
    }
}
