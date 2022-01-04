using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace StackInternship.Presentation.Entities.Screens
{
    public class LoginScreen : IScreen
    {
        public IScreen Render()
        {
            Console.Clear();
            Console.WriteLine("Login");
            Console.ReadKey();
            return new HomeScreen { };
        }
    }
}
