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
            Console.Clear();
            Console.WriteLine(@"Dashboard
Akcije:
1 - Objavljeni resursi
2 - Korisnici
3 - Neodgovoreno
4 - Popularno
5 - Moj profil
6 - Odjavi se
q - Quit");

            switch (Helpers.NumberInput(max: 6))
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
            }
            return null;
        }
    }
}
