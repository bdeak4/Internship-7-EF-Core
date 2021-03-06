using StackInternship.Domain.Enums;
using StackInternship.Presentation.Entities.Interfaces;
using System;

namespace StackInternship.Presentation.Entities.Screens
{
    public class ErrorScreen : IScreen
    {
        public ResponseResultType? Status;

        public IScreen Render()
        {
            Console.Clear();
            Console.WriteLine("Ups! Dogodila se pogreska");

            if (Status != null)
                Console.WriteLine($"Error status {Status}");

            Console.ReadKey();
            return new HomeScreen { };
        }
    }
}
