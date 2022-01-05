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
            Console.WriteLine("Dashboard");
            Console.WriteLine(UserId);
            Console.ReadKey();
            return new HomeScreen { };
        }
    }
}
