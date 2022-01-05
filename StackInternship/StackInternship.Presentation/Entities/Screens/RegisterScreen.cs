using Microsoft.EntityFrameworkCore;
using StackInternship.Domain.Factories;
using StackInternship.Presentation.Entities.Interfaces;
using System;

namespace StackInternship.Presentation.Entities.Screens
{
    public class RegisterScreen : IScreen
    {
        public IScreen Render()
        {
            var userRepository = RepositoryFactory.CreateUserRepository();

            Console.Clear();
            Console.WriteLine("Registracija");

            Console.WriteLine("Unesite korisnicko ime");
            var username = Helpers.TextInput(input => !userRepository.Exists(input));

            Console.WriteLine("Unesite sifru");
            var password = Helpers.PasswordInput();

            return new HomeScreen { };
        }
    }
}
