using StackInternship.Presentation.Entities.Interfaces;
using StackInternship.Presentation.Entities.Screens;

namespace StackInternship.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            IScreen screen = new HomeScreen { };
            while (screen != null)
                screen = screen.Render();
        }
    }
}
