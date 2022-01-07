using StackInternship.Domain.Enums;
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

            switch (action)
            {
                case UserAction.CreateComment:
                    Console.WriteLine("Unesite sadrzaj komentara");
                    commentRepository.Add(UserId, ResourceId, null, Helpers.TextInput(input => true));
                    break;

                case UserAction.CreateSubComment:
                    Console.WriteLine("Unesite sadrzaj komentara");
                    commentRepository.Add(UserId, ResourceId, id, Helpers.TextInput(input => true));
                    break;

                case UserAction.UpvoteResource:
                    resourceRepository.Upvote(ResourceId, UserId);
                    break;

                case UserAction.DownvoteResource:
                    resourceRepository.Downvote(ResourceId, UserId);
                    break;

                case UserAction.UpvoteComment:
                    commentRepository.Upvote(id, UserId);
                    break;

                case UserAction.DownvoteComment:
                    commentRepository.Downvote(id, UserId);
                    break;

                case UserAction.EditComment:
                    Console.WriteLine("Unesite sadrzaj komentara");
                    var editCommentStatus = commentRepository.Edit(id, Helpers.TextInput(input => true));

                    if (editCommentStatus != ResponseResultType.Success)
                        return new ErrorScreen { Status = editCommentStatus };

                    break;

                case UserAction.DeleteComment:
                    var deleteCommentStatus = commentRepository.Delete(id);

                    if (deleteCommentStatus != ResponseResultType.Success)
                        return new ErrorScreen { Status = deleteCommentStatus };

                    break;
            }

            return new ResourceScreen { UserId = UserId, ResourceId = ResourceId };
        }
    }
}
