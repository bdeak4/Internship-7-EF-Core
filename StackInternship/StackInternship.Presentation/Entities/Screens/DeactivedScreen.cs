using StackInternship.Domain.Factories;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace StackInternship.Presentation.Entities.Screens
{
    public class DeactivatedScreen : IScreen
    {
        public int UserId;

        public IScreen Render()
        {
            var userRepository = RepositoryFactory.CreateUserRepository();

            Console.Clear();
            Console.WriteLine("Vas racun je dekativiran");

            var user = userRepository.GetById(UserId);

            Console.WriteLine($"organizator te poslao u samoizolaciju do {user.DeactivatedUntil}");

            Console.ReadKey();

            return new HomeScreen { };
        }
    }
}
