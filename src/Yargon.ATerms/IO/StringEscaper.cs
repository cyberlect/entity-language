using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yargon.ATerms.IO
{
    internal static class StringEscaper
    {
        /// <summary>
        /// Escapes the specified string.
        /// </summary>
        /// <param name="input">The string to escape.</param>
        /// <param name="sequences">A dictionary with for each character to escape, the escape sequence.</param>
        /// <returns>The escaped string.</returns>
        public static string Escape(string input, IReadOnlyDictionary<char, string> sequences)
        {
            #region Contract
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (sequences == null)
                throw new ArgumentNullException(nameof(sequences));
            #endregion

            StringBuilder sb = new StringBuilder();
            foreach (char ch in input)
            {
                string replacement;
                if (sequences.TryGetValue(ch, out replacement))
                    sb.Append(replacement);
                else
                    sb.Append(ch);
            }
            return sb.ToString();
        }
    }
}
