using StackInternship.Data.Entities.Enums;
using StackInternship.Domain.Enums;
using StackInternship.Domain.Factories;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackInternship.Presentation.Entities.Screens
{
    public class UserDeactivatedScreen : IScreen
    {
        public int UserId;

        public IScreen Render()
        {
            var userRepository = RepositoryFactory.CreateUserRepository();

            Console.Clear();

            var users = userRepository.GetAll().Where(u => 
                u.DeactivatedUntil != null && u.DeactivatedUntil > DateTime.Now
            ).ToList();

            Console.WriteLine($@"Deaktivirani interni
{string.Join("\n", users.Select((u, i) => $"{i + 1} - {Helpers.PrintUsername(u, 0)} do {u.DeactivatedUntil}").ToList())}
{users.Count + 1} - Povratak u dashboard
q - Quit");

            var input = Helpers.NumberInput(max: users.Count + 1);

            if (input == null)
                return null;

            if (input == (users.Count + 1))
                return new DashboardScreen { UserId = UserId };

            var status = userRepository.UnDeactivate(input.GetValueOrDefault());

            if (status == ResponseResultType.Success)
                return new UserDeactivationScreen { UserId = UserId };

            return new ErrorScreen { Status = status };
        }
    }
}
