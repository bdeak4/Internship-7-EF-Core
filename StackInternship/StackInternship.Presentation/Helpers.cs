using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackInternship.Presentation
{
    public static class Helpers
    {
        public static int? NumberInput(List<int> permittedValues)
        {
            while (true)
            {
                Console.Write("Unos: ");
                var input = Console.ReadLine();

                if (input == "q")
                    return null;

                var valid = int.TryParse(input, out int value);

                if (valid && permittedValues.IndexOf(value) != -1)
                    return value;
                
                Console.WriteLine("Unos nije validan. Pokusajte ponovo.");
            }
        }

        public static string TextInput(Func<string, bool> validate)
        {
            while (true)
            {
                Console.Write("Unos: ");
                var input = Console.ReadLine();

                if (validate(input))
                    return input;

                Console.WriteLine("Unos nije validan. Pokusajte ponovo.");
            }
        }
    }
}
