using StackInternship.Data.Entities.Models;
using StackInternship.Domain.Enums;
using StackInternship.Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackInternship.Presentation
{
    public static class Helpers
    {
        public static int? NumberInput(int max)
        {
            while (true)
            {
                Console.Write("Unos: ");
                var input = Console.ReadLine();

                if (input == "q")
                    return null;

                var valid = int.TryParse(input, out int value);

                if (valid && value >= 1 && value <= max)
                    return value;

                Console.WriteLine("Unos nije validan. Pokusajte ponovo.");
            }
        }

        public static string TextInput(Func<string, bool> validate) => GenericTextInput(validate, Console.ReadLine);
        public static string PasswordInput(Func<string, bool> validate) => GenericTextInput(validate, PasswordRead);

        private static string GenericTextInput(Func<string, bool> validate, Func<string> read)
        {
            while (true)
            {
                Console.Write("Unos: ");
                var input = read();

                if (validate(input))
                    return input;

                Console.WriteLine("Unos nije validan. Pokusajte ponovo.");
            }
        }

        private static string PasswordRead()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            Console.Write("\n");

            return pass;
        }

        public static (int?, int, UserAction) NumberInputWithPermittedValues(int max, List<(int, int, UserAction)> permittedValues)
        {
            var num = NumberInput(max);

            if (num == null)
                return (null, 0, UserAction.NoAction);

            foreach (var pv in permittedValues)
                if (pv.Item1 == num)
                    return pv;

            return (num, 0, UserAction.NoAction);
        }

        public static string PrintResources(ICollection<Resource> resources, int firstIndex, int userId)
        {
            var userRepository = RepositoryFactory.CreateUserRepository();

            var rows = resources.Select((r, i) =>
                $"{i + firstIndex} - {PrintResourceMetadata(r, userId)} {r.Title}"
            );

            return string.Join("\n", rows);
        }

        public static string PrintResourceMetadata(Resource re, int currentUserId)
        {
            var r = "\u001b[31m";
            var g = "\u001b[32m";
            var y = "\u001b[33m";
            var n = "\u001b[0m";
            return
                $"{y}[{re.Category}]{n} " +
                $"{g}↑{re.Upvotes.Count}{n} " +
                $"{r}↓{re.Downvotes.Count}{n} " +
                $"{{{re.Views.Count}}} " +
                $"{PrintUsername(re.User, currentUserId)} " +
                $"({re.CreatedAt})";

        }

        public static string PrintResourceActions(
            Resource r,
            int currentUserId,
            List<(int, int, UserAction)> permittedValues,
            int startIndex,
            out int index)
        {
            index = startIndex;
            var userRepository = RepositoryFactory.CreateUserRepository();

            var output = "";

            if (userRepository.CanCreateComment(currentUserId))
            {
                var i = ++index;
                permittedValues.Add((i, r.Id, UserAction.CreateComment));
                output += $"{i} - Komentiraj\n";
            }

            if (userRepository.CanUpvoteResource(currentUserId, r.Id))
            {
                var i = ++index;
                permittedValues.Add((i, r.Id, UserAction.UpvoteResource));
                output += $"{i} - Upvote post\n";
            }

            if (userRepository.CanDownvoteResource(currentUserId, r.Id))
            {
                var i = ++index;
                permittedValues.Add((i, r.Id, UserAction.DownvoteResource));
                output += $"{i} - Downvote post\n";
            }

            return output;
        }

        public static string PrintComments(
            ICollection<Comment> comments,
            int currentUserId,
            List<(int, int, UserAction)> permittedValues,
            int startIndex,
            out int index)
        {
            index = startIndex;

            var output = "";

            foreach (var c in comments.Where(c => c.ParentId == null))
                output += PrintComment(
                    c,
                    currentUserId,
                    permittedValues,
                    index,
                    out index,
                    0);

            return output;
        }

        public static string PrintComment(
            Comment c,
            int currentUserId,
            List<(int, int, UserAction)> permittedValues,
            int startIndex,
            out int index,
            int indentationLevel)
        {
            index = startIndex;
            var indentation = string.Concat(Enumerable.Repeat("\t", indentationLevel));

            var output = @$"{indentation}{c.Content}
{indentation}{PrintCommentMetadata(c, currentUserId)}
{PrintCommentActions(
    c,
    currentUserId,
    permittedValues,
    index,
    out index,
    indentation)}
";

            foreach (var child in c.Children)
                output += PrintComment(
                    child,
                    currentUserId,
                    permittedValues,
                    index,
                    out index,
                    indentationLevel + 1) + "\n";

            return output;

        }

        public static string PrintCommentMetadata(Comment c, int currentUserId)
        {
            var r = "\u001b[31m";
            var g = "\u001b[32m";
            var n = "\u001b[0m";
            return
                $"{g}↑{c.Upvotes.Count}{n} " +
                $"{r}↓{c.Downvotes.Count}{n} " +
                $"{PrintUsername(c.User, currentUserId)} " +
                $"({c.CreatedAt})";
        }

        public static string PrintCommentActions(
            Comment c,
            int currentUserId,
            List<(int, int, UserAction)> permittedValues,
            int startIndex,
            out int index,
            string indentation)
        {
            index = startIndex;
            var userRepository = RepositoryFactory.CreateUserRepository();

            var output = "";

            if (userRepository.CanCreateSubComment(currentUserId))
            {
                var i = ++index;
                permittedValues.Add((i, c.Id, UserAction.CreateSubComment));
                output += $"{indentation}{i} - Odgovori na komentar\n";
            }

            if (userRepository.CanUpvoteComment(currentUserId, c.Id))
            {
                var i = ++index;
                permittedValues.Add((i, c.Id, UserAction.UpvoteComment));
                output += $"{indentation}{i} - Upvote komentar\n";
            }

            if (userRepository.CanDownvoteComment(currentUserId, c.Id))
            {
                var i = ++index;
                permittedValues.Add((i, c.Id, UserAction.DownvoteComment));
                output += $"{indentation}{i} - Downvote komentar\n";
            }

            if (userRepository.CanEditComment(currentUserId, c.Id))
            {
                var i = ++index;
                permittedValues.Add((i, c.Id, UserAction.EditComment));
                output += $"{indentation}{i} - Uredi komentar\n";
            }

            if (userRepository.CanDeleteComment(currentUserId))
            {
                var i = ++index;
                permittedValues.Add((i, c.Id, UserAction.DeleteComment));
                output += $"{indentation}{i} - Obrisi comment\n";
            }

            return output;
        }

        public static string PrintUsername(User u, int currentUserId)
        {
            var userRepository = RepositoryFactory.CreateUserRepository();

            var b = "\u001b[36m";
            var m = "\u001b[35m";
            var n = "\u001b[0m";

            var color = n;

            if (userRepository.IsTrusted(u.Id))
                color = b;

            if (userRepository.IsOrganizator(u.Id))
                color = m;

            return $"{color}[{(u.Id == currentUserId ? "you" : u.Username)}]{n}";
        }


    }
}
