using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace StackInternship.Presentation.Entities.Screens
{
    public class RegisterScreen : IScreen
    {
        public IScreen Render()
        {
            Console.Clear();
            Console.WriteLine("Register");
            Console.ReadKey();
            return new HomeScreen { };
        }
    }
}
