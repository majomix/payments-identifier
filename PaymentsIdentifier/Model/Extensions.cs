/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Collections.Generic;
using System.Linq;

namespace PaymentsIdentifier.Model
{
    internal static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null) return true;

            var collection = enumerable as ICollection<T>;
            if (collection != null)
            {
                return collection.Count < 1;
            }

            return !enumerable.Any();
        }

        public static List<string> ExtractInvoiceNumbers(this string input, string substring = "62", int length = 10)
        {
            IList<int> allIndices = new List<int>();
            List<string> outputs = new List<string>();
            int index = input.IndexOf(substring);
            while (index != -1)
            {
                allIndices.Add(index);
                index = input.IndexOf(substring, index + substring.Length);
            }

            foreach (int currentIndex in allIndices)
            {
                if(currentIndex + length <= input.Length)
                {
                    string tenDigits = input.Substring(currentIndex, length);
                    if (!tenDigits.Any(_ => _ < '0' || _ > '9') && !outputs.Contains(tenDigits)) outputs.Add(tenDigits);
                }
            }

            return outputs;
        }
    }
}
