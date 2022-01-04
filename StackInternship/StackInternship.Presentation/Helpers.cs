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

        internal static void SmallNumberInput()
        {
            throw new NotImplementedException();
        }
    }
}
