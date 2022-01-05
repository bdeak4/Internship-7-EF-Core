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

            Console.WriteLine("Unesite korisnicko ime za vas racun");
            var username = Helpers.TextInput(input => !userRepository.Exists(input));

            Console.WriteLine("Unesite sifru za vas racun");
            var password = Helpers.TextInput(input => true);

            return new HomeScreen { };
        }
    }
}
