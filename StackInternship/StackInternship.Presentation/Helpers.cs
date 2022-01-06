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
            var b = "\u001b[34m";
            var y = "\u001b[33m";
            var n = "\u001b[0m";
            return
                $"{y}[{re.Category}]{n} " +
                $"{g}↑{re.Upvotes.Count}{n} " +
                $"{r}↓{re.Downvotes.Count}{n} " +
                $"{{{re.Views.Count}}} " +
                $"{b}[{(re.User.Id == currentUserId ? "you" : re.User.Username)}]{n} " +
                $"({re.CreatedAt})";

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
                output += PrintComment(c, currentUserId, 0);

            return output;
        }

        public static string PrintComment(Comment c, int currentUserId, int indentationLevel)
        {
            var indentation = string.Concat(Enumerable.Repeat("\t", indentationLevel));

            return @$"{indentation}{c.Content}
{indentation}{PrintCommentMetadata(c, currentUserId)}
{indentation}// actions

{string.Join("\n", c.Children.Select(c => PrintComment(c, currentUserId, indentationLevel + 1)))}";
        }

        public static string PrintCommentMetadata(Comment c, int currentUserId)
        {
            var r = "\u001b[31m";
            var g = "\u001b[32m";
            var b = "\u001b[34m";
            var n = "\u001b[0m";
            return
                $"{g}↑{c.Upvotes.Count}{n} " +
                $"{r}↓{c.Downvotes.Count}{n} " +
                $"{b}[{(c.User.Id == currentUserId ? "you" : c.User.Username)}]{n} " +
                $"({c.CreatedAt})";
        }


    }
}
