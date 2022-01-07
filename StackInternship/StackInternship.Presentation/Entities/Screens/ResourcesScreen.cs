using StackInternship.Data.Entities.Enums;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Linq;

namespace StackInternship.Presentation.Entities.Screens
{
    public class ResourcesScreen : IScreen
    {
        public int UserId;

        public IScreen Render()
        {
            Console.Clear();

            var categories = Enum.GetValues(typeof(ResourceCategory)).Cast<ResourceCategory>().ToList();

            Console.WriteLine($@"Resursi
Kategorije:
{string.Join("\n", categories.Select((c, i) => $"{i + 1} - {c}").ToList())}
{categories.Count + 1} - Povratak u dashboard
q - Quit");

            var input = Helpers.NumberInput(max: categories.Count + 1);

            if (input == null)
                return null;

            if (input == (categories.Count + 1))
                return new DashboardScreen { UserId = UserId };

            return new ResourcesByCategoryScreen
            {
                UserId = UserId,
                ResourceCategory = categories[input.GetValueOrDefault() - 1]
            };
        }
    }
}
