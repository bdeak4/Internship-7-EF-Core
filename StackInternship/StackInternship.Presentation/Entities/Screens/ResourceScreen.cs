﻿using StackInternship.Domain.Enums;
using StackInternship.Domain.Factories;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackInternship.Presentation.Entities.Screens
{
    public class ResourceScreen : IScreen
    {
        public int UserId;
        public int ResourceId;

        public IScreen Render()
        {
            var resourceRepository = RepositoryFactory.CreateResourceRepository();
            var commentRepository = RepositoryFactory.CreateCommentRepository();

            Console.Clear();

            var resource = resourceRepository.GetById(ResourceId);

            resourceRepository.View(ResourceId, UserId);

            var index = 0;
            var permittedValues = new List<(int, int, UserAction)> { };

            Console.WriteLine($@"{resource.Title}
{string.Concat(Enumerable.Repeat("-", resource.Title.Length))}
{Helpers.PrintResourceMetadata(resource, UserId)}

{resource.Content}

{Helpers.PrintResourceActions(
    resource,
    UserId,
    permittedValues,
    index,
    out index)}
--

{Helpers.PrintComments(
    resource.Comments,
    UserId,
    permittedValues,
    index,
    out index)}
Akcije:
{++index} - Povratak na listu postova
q - Quit");

            var (input, id, action) = Helpers.NumberInputWithPermittedValues(index, permittedValues);

            if (input == null)
                return null;
            
            if (input == index)
                return new ResourcesByCategoryScreen { UserId = UserId, ResourceCategory = resource.Category };

            switch(action)
            {
                //case UserAction.CreateComment:

                //case UserAction.CreateSubComment:

                case UserAction.UpvoteResource:
                    resourceRepository.Upvote(ResourceId, UserId);
                    return new ResourceScreen { UserId = UserId, ResourceId = ResourceId };

                case UserAction.DownvoteResource:
                    resourceRepository.Downvote(ResourceId, UserId);
                    return new ResourceScreen { UserId = UserId, ResourceId = ResourceId };

                case UserAction.UpvoteComment:
                    commentRepository.Upvote(id, UserId);
                    return new ResourceScreen { UserId = UserId, ResourceId = ResourceId };

                case UserAction.DownvoteComment:
                    commentRepository.Downvote(id, UserId);
                    return new ResourceScreen { UserId = UserId, ResourceId = ResourceId };

                //case UserAction.EditComment:

                //case UserAction.DeleteComment:
            }

            return new ResourcesByCategoryScreen { UserId = UserId, ResourceCategory = resource.Category };
        }
    }
}
