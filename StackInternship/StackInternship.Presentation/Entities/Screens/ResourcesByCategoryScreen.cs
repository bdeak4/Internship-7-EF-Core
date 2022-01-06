using StackInternship.Data.Entities.Enums;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackInternship.Presentation.Entities.Screens
{
    public class ResourcesByCategoryScreen : IScreen
    {
        public int UserId;
        public ResourceCategory ResourceCategory;

        public IScreen Render()
        {
            Console.Clear();
            Console.WriteLine($@"Resursi u kategoriji {ResourceCategory}
q - Quit");

            switch (Helpers.NumberInput(max: 5))
            {
                case 1:
                    return new HomeScreen { };

                case 2:
                    return new HomeScreen { };

                case 3:
                    return new HomeScreen { };

                case 4:
                    return new HomeScreen { };

                case 5:
                    return new HomeScreen { };
            }
            return null;
        }
    }
}
