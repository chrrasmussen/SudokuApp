using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    public static class IEnumerableExtensions
    {
        public static string GetFormattedString<T>(this IEnumerable<T> values, string prefix = "{ ", string separator = ", ", string postfix = " }")
        {
            StringBuilder formattedText = new StringBuilder(prefix);

            foreach (T value in values)
            {
                if (formattedText.Length > prefix.Length)
                {
                    formattedText.Append(separator);
                }

                formattedText.Append(value);
            }

            formattedText.Append(postfix);

            return formattedText.ToString();
        }
    }
}
