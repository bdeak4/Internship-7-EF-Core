using StackInternship.Data.Entities.Enums;
using StackInternship.Domain.Enums;
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
            var userRepository = RepositoryFactory.CreateUserRepository();

            Console.Clear();

            var resources = resourceRepository.GetByCategory(ResourceCategory).ToList();

            var index = resources.Count;

            Console.WriteLine($@"Resursi u kategoriji {ResourceCategory}
{Helpers.PrintResources(resources, 1, UserId)}
{++index} - Povratak na kategorije {(userRepository.IsOrganizator(UserId) ? $"\n{++index} - Dodaj novi resurs" : "")}
q - Quit");

            var input = Helpers.NumberInput(max: index);

            if (input == null)
                return null;

            if (input == (resources.Count + 1))
                return new ResourcesScreen { UserId = UserId };

            if (input == index)
            {
                Console.WriteLine("Unesite naslov resursa");
                var title = Helpers.TextInput(input => input.Length > 0);

                Console.WriteLine("Unesite text resursa");
                var content = Helpers.TextInput(input => input.Length > 0);

                var (id, status) = resourceRepository.Create(title, content, ResourceCategory, UserId);

                if (status == ResponseResultType.Success)
                    return new ResourceScreen { UserId = UserId, ResourceId = id };

                return new ErrorScreen { Status = status };
            }

            var resource = resources[input.GetValueOrDefault() - 1];

            return new ResourceScreen { UserId = UserId, ResourceId = resource.Id };
        }
    }
}
