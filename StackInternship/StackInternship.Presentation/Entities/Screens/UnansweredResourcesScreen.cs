using StackInternship.Domain.Factories;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Linq;

namespace StackInternship.Presentation.Entities.Screens
{
    public class UnansweredResourcesScreen : IScreen
    {
        public int UserId;

        public IScreen Render()
        {
            var resourceRepository = RepositoryFactory.CreateResourceRepository();

            Console.Clear();

            var resources = resourceRepository.GetUnanswered().ToList();

            Console.WriteLine($@"Neodgovoreni resursi
{Helpers.PrintResources(resources, 1, UserId)}
{resources.Count + 1} - Povratak u dashboard
q - Quit");

            var input = Helpers.NumberInput(max: resources.Count + 1);

            if (input == null)
                return null;

            if (input == (resources.Count + 1))
                return new DashboardScreen { UserId = UserId };

            var resource = resources[input.GetValueOrDefault() - 1];

            return new ResourceScreen { UserId = UserId, ResourceId = resource.Id };
        }
    }
}
