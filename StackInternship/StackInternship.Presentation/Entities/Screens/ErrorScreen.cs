using StackInternship.Domain.Enums;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace StackInternship.Presentation.Entities.Screens
{
    public class ErrorScreen : IScreen
    {
        public ResponseResultType? Status;

        public IScreen Render()
        {
            Console.Clear();
            Console.WriteLine("Ups! Dogodila se pogreska");
            Console.ReadKey();
            return new HomeScreen { };
        }
    }
}
