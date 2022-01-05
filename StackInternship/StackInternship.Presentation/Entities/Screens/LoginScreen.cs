using StackInternship.Domain.Factories;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace StackInternship.Presentation.Entities.Screens
{
    public class LoginScreen : IScreen
    {
        public IScreen Render()
        {
            var userRepository = RepositoryFactory.CreateUserRepository();

            Console.Clear();
            Console.WriteLine("Login");

            Console.WriteLine("Unesite korisnicko ime");
            var username = Helpers.TextInput(input => userRepository.Exists(input));

            Console.WriteLine("Unesite sifru");
            var password = Helpers.PasswordInput(input => userRepository.CheckPassword(username, input));

            return new DashboardScreen { UserId = userRepository.GetIdByUsername(username) };
        }
    }
}
