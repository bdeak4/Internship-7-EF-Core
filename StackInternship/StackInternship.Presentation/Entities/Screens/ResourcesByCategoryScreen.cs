using StackInternship.Data.Entities.Enums;
using StackInternship.Domain.Factories;
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
            var resourceRepository = RepositoryFactory.CreateResourceRepository();

            Console.Clear();

            var resources = resourceRepository.GetByCategory(ResourceCategory);

            Console.WriteLine($@"Resursi u kategoriji {ResourceCategory}
{Helpers.PrintResources(resources, 1, UserId)}
{resources.Count + 1} - Povratak na kategorije
q - Quit");

            var input = Helpers.NumberInput(max: resources.Count + 1);

            if (input == null)
                return null;

            if (input == (resources.Count + 1))
                return new ResourcesScreen { UserId = UserId };

            // resourcescreen
            return null;
        }
    }
}
