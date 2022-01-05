using Microsoft.EntityFrameworkCore;
using StackInternship.Data.Entities;
using StackInternship.Domain.Repositories;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace StackInternship.Presentation.Entities.Screens
{
    public class RegisterScreen : IScreen
    {
        readonly StackInternshipDbContext dbContext = (new StackInternshipContextFactory()).CreateDbContext(null);

        public IScreen Render()
        {
            var userRepository = new UserRepository(dbContext);

            Console.Clear();
            Console.WriteLine("Registracija");

            Console.WriteLine("Unesite korisnicko ime za vas racun");
            var username = Helpers.TextInput(input => !userRepository.Exists(input));

            var password = Helpers.TextInput(input => true);

            return new HomeScreen { };
        }
    }
}
