using StackInternship.Presentation.Entities.Interfaces;
using System;

namespace StackInternship.Presentation.Entities.Screens
{
    public class HomeScreen : IScreen
    {
        public IScreen Render()
        {
            Console.Clear();
            Console.WriteLine(@"Dobrodosli na StackInternship
Akcije:
1 - Login
2 - Registracija
q - Quit");

            switch (Helpers.NumberInput(max: 2))
            {
                case 1:
                    return new LoginScreen { };

                case 2:
                    return new RegisterScreen { };
            }
            return null;
        }
    }
}
