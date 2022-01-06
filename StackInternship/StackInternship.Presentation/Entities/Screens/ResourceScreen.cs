﻿using StackInternship.Domain.Factories;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackInternship.Presentation.Entities.Screens
{
    public class ResourceScreen : IScreen
    {
        public int UserId;
        public int ResourceId;

        public IScreen Render()
        {
            var resourceRepository = RepositoryFactory.CreateResourceRepository();

            Console.Clear();

            var resource = resourceRepository.GetById(ResourceId);

            var index = 3;

            Console.WriteLine($@"{resource.Title}
{string.Concat(Enumerable.Repeat("-", resource.Title.Length))}
{Helpers.PrintResourceMetadata(resource, UserId)}

{resource.Content}

// actions
--

{Helpers.PrintComments(
    resource.Comments,
    UserId,
    out Dictionary<string,
    List<int>> permittedCommentValues,
    index,
    out index)}
Akcije:
{index++} - Povratak na listu postova
q - Quit");
            Console.ReadKey();
            return new ResourcesByCategoryScreen { UserId = UserId, ResourceCategory = resource.Category };

            //var (input, action) = Helpers.NumberInputWithPermittedValues(permittedCommentValues);
            //
            //if (input == null)
            //    return null;
            //
            //if (input == index)
            //    return new ResourcesByCategoryScreen { UserId = UserId, ResourceCategory = resource.Category };
            //
            // handle actions
            //return new HomeScreen { };
        }
    }
}
