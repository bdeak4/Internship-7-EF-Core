using StackInternship.Data.Entities.Models;
using StackInternship.Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            out Dictionary<string, List<int>> permittedResourceValues, 
            int startIndex,
            out int index)
        {
            index = startIndex;
            permittedResourceValues = new Dictionary<string, List<int>> { };
            var userRepository = RepositoryFactory.CreateUserRepository();

            var output = "";

            if (userRepository.CanCreateComment(currentUserId))
            {
                var i = ++index;
                permittedResourceValues["create-comment"] = new List<int> { i };
                output += $"{i} - Komentiraj\n";
            }

            if (userRepository.CanUpvoteResource(currentUserId, r.Id))
            {
                var i = ++index;
                permittedResourceValues["upvote-resource"] = new List<int> { i };
                output += $"{i} - Upvote post\n";
            }

            if (userRepository.CanDownvoteResource(currentUserId, r.Id))
            {
                var i = ++index;
                permittedResourceValues["downvote-resource"] = new List<int> { i };
                output += $"{i} - Downvote post\n";
            }

            if (userRepository.CanEditResource(currentUserId, r.Id))
            {
                var i = ++index;
                permittedResourceValues["edit-resource"] = new List<int> { i };
                output += $"{i} - Uredi post\n";
            }

            if (userRepository.CanDeleteResource(currentUserId, r.Id))
            {
                var i = ++index;
                permittedResourceValues["delete-resource"] = new List<int> { i };
                output += $"{i} - Obrisi post\n";
            }

            return output;
        }

        public static string PrintComments(
            ICollection<Comment> comments,
            int currentUserId,
            out Dictionary<string, List<int>> permittedCommentValues,
            int startIndex,
            out int index)
        {
            permittedCommentValues = new Dictionary<string, List<int>> { };
            index = startIndex;

            var output = "";

            foreach (var c in comments.Where(c => c.ParentId == null))
                output += PrintComment(
                    c,
                    currentUserId, 
                    permittedCommentValues, 
                    out permittedCommentValues, 
                    index, 
                    out index, 
                    0);

            return output;
        }

        public static string PrintComment(
            Comment c,
            int currentUserId,
            Dictionary<string, List<int>> startPermittedCommentValues,
            out Dictionary<string, List<int>> permittedCommentValues,
            int startIndex,
            out int index,
            int indentationLevel)
        {
            permittedCommentValues = startPermittedCommentValues;
            index = startIndex;
            var indentation = string.Concat(Enumerable.Repeat("\t", indentationLevel));

            var output =  @$"{indentation}{c.Content}
{indentation}{PrintCommentMetadata(c, currentUserId)}
{PrintCommentActions(
    c,
    currentUserId,
    permittedCommentValues,
    out permittedCommentValues,
    index,
    out index,
    indentation)}
";

            foreach (var child in c.Children)
                output += PrintComment(
                    child,
                    currentUserId,
                    permittedCommentValues,
                    out permittedCommentValues,
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
            Dictionary<string, List<int>> startPermittedCommentValues,
            out Dictionary<string, List<int>> permittedCommentValues,
            int startIndex,
            out int index,
            string indentation)
        {
            index = startIndex;
            permittedCommentValues = startPermittedCommentValues;
            var userRepository = RepositoryFactory.CreateUserRepository();

            var output = "";

            if (userRepository.CanCreateSubComment(currentUserId))
            {
                var i = ++index;
                if (permittedCommentValues.ContainsKey("create-subcomment"))
                    permittedCommentValues["create-subcomment"].Append(i);
                else
                    permittedCommentValues["create-subcomment"] = new List<int> { i };
                output += $"{indentation}{i} - Odgovori na komentar\n";
            }

            if (userRepository.CanUpvoteComment(currentUserId, c.Id))
            {
                var i = ++index;
                if (permittedCommentValues.ContainsKey("upvote-comment"))
                    permittedCommentValues["upvote-comment"].Append(i);
                else
                    permittedCommentValues["upvote-comment"] = new List<int> { i };
                output += $"{indentation}{i} - Upvote komentar\n";
            }

            if (userRepository.CanDownvoteComment(currentUserId, c.Id))
            {
                var i = ++index;
                if (permittedCommentValues.ContainsKey("downvote-comment"))
                    permittedCommentValues["downvote-comment"].Append(i);
                else
                    permittedCommentValues["downvote-comment"] = new List<int> { i };
                output += $"{indentation}{i} - Downvote komentar\n";
            }

            if (userRepository.CanEditComment(currentUserId, c.Id))
            {
                var i = ++index;
                if (permittedCommentValues.ContainsKey("edit-comment"))
                    permittedCommentValues["edit-comment"].Append(i);
                else
                    permittedCommentValues["edit-comment"] = new List<int> { i };
                output += $"{indentation}{i} - Uredi komentar\n";
            }

            if (userRepository.CanDeleteComment(currentUserId))
            {
                var i = ++index;
                if (permittedCommentValues.ContainsKey("delete-comment"))
                    permittedCommentValues["delete-comment"].Append(i);
                else
                    permittedCommentValues["delete-comment"] = new List<int> { i };
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
