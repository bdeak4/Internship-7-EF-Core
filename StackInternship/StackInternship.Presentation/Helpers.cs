using StackInternship.Data.Entities.Models;
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
            var rows = resources.Select((r, i) => 
                $"{i + firstIndex} - ↑{r.Upvotes.Count} ↓{r.Downvotes.Count} " +
                $"{{{r.Views.Count}}} {(r.User.IsOrganizer ? "org " : "")}" +
                $"[{(r.User.Id == userId ? "you" : r.User.Username)}] ({r.CreatedAt})"
            );
            return string.Join("\n", rows);
        }

    }
}
