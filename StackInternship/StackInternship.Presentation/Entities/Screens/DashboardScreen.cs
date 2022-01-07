using StackInternship.Domain.Factories;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace StackInternship.Presentation.Entities.Screens
{
    public class DashboardScreen : IScreen
    {
        public int UserId;

        public IScreen Render()
        {
            var userRepository = RepositoryFactory.CreateUserRepository();

            Console.Clear();

            var index = 0;

            Console.WriteLine($@"Dashboard
Akcije:
{++index} - Objavljeni resursi
{++index} - Korisnici
{++index} - Neodgovoreno
{++index} - Popularno
{++index} - Moj profil
{++index} - Odjavi se {(userRepository.IsOrganizator(UserId) ? $"\n{++index} - Deaktiviraj racun interna\n{++index} - Deaktivirani pripravnici" : "")}
q - Quit");

            switch (Helpers.NumberInput(max: index))
            {
                case 1:
                    return new ResourcesScreen { UserId = UserId };

                case 2:
                    return new UsersScreen { UserId = UserId };

                case 3:
                    return new UnansweredResourcesScreen { UserId = UserId };

                case 4:
                    return new PopularResourcesScreen { UserId = UserId };

                case 5:
                    return new UserProfileScreen { UserId = UserId, ProfileUserId = UserId };

                case 6:
                    return new HomeScreen { };

                case 7:
                    return new UserDeactivationScreen { UserId = UserId };

                case 8:
                    return new UserDeactivatedScreen { UserId = UserId };
            }
            return null;
        }
    }
}
