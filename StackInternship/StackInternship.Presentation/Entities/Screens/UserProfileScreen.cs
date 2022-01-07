using StackInternship.Domain.Enums;
using StackInternship.Domain.Factories;
using StackInternship.Presentation.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace StackInternship.Presentation.Entities.Screens
{
    public class UserProfileScreen : IScreen
    {
        public int UserId;
        public int ProfileUserId;

        public IScreen Render()
        {
            var userRepository = RepositoryFactory.CreateUserRepository();
            var user = userRepository.GetById(ProfileUserId);

            var index = 0;

            Console.Clear();
            Console.WriteLine($@"Profil korisnika: {Helpers.PrintUsername(user, 0)}
Reputacija: {userRepository.CalculateRep(ProfileUserId)}
Korisnik od: {user.CreatedAt}
Broj resursa: {user.Resources.Count}
Broj komentara: {user.Comments.Count}
Broj primljenih upvotea: {userRepository.GetReceivedUpvotesCount(ProfileUserId)}
Broj primljenih downvotea: {userRepository.GetReceivedDownvotesCount(ProfileUserId)}
Broj poslanih upvotea: {user.Upvotes.Count}
Broj poslanih downvotea: {user.Downvotes.Count}

Akcije:
{(UserId == ProfileUserId ? $"{++index} - promjeni korisnicko ime" : "\b" )}
{++index} - povratak na listu korisnika
q - Quit");

            var input = Helpers.NumberInput(max: index);

            if (input == null)
                return null;

            if (input == index)
                    return new DashboardScreen { UserId = UserId }; //todo

            Console.WriteLine("Unesite sifru");
            Helpers.PasswordInput(input => userRepository.CheckPassword(user.Username, input));
            
            Console.WriteLine("Unesite novo korisnicko ime");
            var username = Helpers.TextInput(input => !userRepository.Exists(input));

            var status = userRepository.Edit(ProfileUserId, username);

            if (status == ResponseResultType.Success)
                return new UserProfileScreen { UserId = UserId, ProfileUserId = ProfileUserId };

            return new ErrorScreen { Status = status };
        }
    }
}
