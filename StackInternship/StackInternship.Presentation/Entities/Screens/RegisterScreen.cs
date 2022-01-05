using Microsoft.EntityFrameworkCore;
using StackInternship.Domain.Enums;
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
            var password = Helpers.PasswordInput(input => true);

            var (userId, status) = userRepository.Create(username, password);

            if (status == ResponseResultType.Success)
                return new DashboardScreen { UserId = userId };

            return new ErrorScreen { Status = status };
        }
    }
}
