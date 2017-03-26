using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Yargon.ATerms.IO
{
    /// <summary>
    /// Extension methods for the <see cref="TextReader"/>.
    /// </summary>
    internal static class TextReaderExtensions
    {
        /// <summary>
        /// Reads a character from the reader;
        /// or throws an exception when the end of the stream has been reached unexpectedly.
        /// </summary>
        /// <param name="reader">The reader to read from.</param>
        /// <returns>The read character.</returns>
        public static char ReadOrFail(this TextReader reader)
        {
            #region Contract
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            #endregion

            int ch = reader.Read();
            if (ch == -1)
                throw new EndOfStreamException("Unexpected end of stream.");
            return (char)ch;
        }

        /// <summary>
        /// Peeks a character from the reader;
        /// or throws an exception when the end of the stream has been reached unexpectedly.
        /// </summary>
        /// <param name="reader">The reader to read from.</param>
        /// <returns>The read character.</returns>
        public static char PeekOrFail(this TextReader reader)
        {
            #region Contract
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            #endregion

            int ch = reader.Peek();
            if (ch == -1)
                throw new EndOfStreamException("Unexpected end of stream.");
            return (char)ch;
        }

        /// <summary>
        /// Expects to read a specific character from the reader;
        /// or throws an exception when the read character was different,
        /// or when the end of the stream has been reached.
        /// </summary>
        /// <param name="reader">The reader to read from.</param>
        /// <param name="expected">The expected character.</param>
        /// <param name="expectedName">The name of the expected character; or <see langword="null"/>.</param>
        /// <returns>The read character.</returns>
        public static char ReadExpected(this TextReader reader, char expected, [CanBeNull] string expectedName = null)
        {
            #region Contract
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            #endregion

            return ReadExpected(reader, new [] { expected }, null);
        }

        /// <summary>
        /// Expects to read a specific character from the reader;
        /// or throws an exception when the read character was different,
        /// or when the end of the stream has been reached.
        /// </summary>
        /// <param name="reader">The reader to read from.</param>
        /// <param name="expected">The expected characters.</param>
        /// <param name="expectedName">The name of the expected character; or <see langword="null"/>.</param>
        /// <returns>The read character.</returns>
        public static char ReadExpected(this TextReader reader, IReadOnlyCollection<Char> expected, [CanBeNull] string expectedName = null)
        {
            #region Contract
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (expected == null)
                throw new ArgumentNullException(nameof(expected));
            #endregion

            char ch = reader.ReadOrFail();
            if (!expected.Contains(ch) && expected.Any())
            {
                string expectedStr = expectedName ?? (expected.Count == 1 ? $"'{expected.Single()}'" : $"one of {String.Join(", ", expected.Select(c => $"'{c}'"))}");
                throw new TermParseException($"Expected {expectedStr}, got '{ch}' character.");
            }
            return ch;
        }

        /// <summary>
        /// Expects to read a specific character from the reader;
        /// or throws an exception when the read character was different,
        /// or when the end of the stream has been reached.
        /// </summary>
        /// <param name="reader">The reader to read from.</param>
        /// <param name="expected">The expected characters.</param>
        /// <returns>The read character.</returns>
        public static char ReadExpected(this TextReader reader, params char[] expected)
        {
            #region Contract
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            #endregion

            return ReadExpected(reader, (IReadOnlyCollection<Char>) expected, null);
        }

        /// <summary>
        /// Tries to peek to see if there is a next character.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns><see langword="true"/> if there is a next character;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool TryPeek(this TextReader reader)
        {
            #region Contract
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            #endregion

            return TryPeek(reader, _ => true);
        }

        /// <summary>
        /// Tries to peek to see if the next character is the expected character.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="expected">The expected character.</param>
        /// <returns><see langword="true"/> if the next character is the expected character;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool TryPeek(this TextReader reader, char expected)
        {
            #region Contract
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            #endregion

            return TryPeek(reader, c => c == expected);
        }

        /// <summary>
        /// Tries to peek to see if the next character is one of the expected characters.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="expected">The expected characters.</param>
        /// <returns><see langword="true"/> if the next character is one of the expected characters;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool TryPeek(this TextReader reader, params char[] expected)
        {
            #region Contract
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            #endregion

            return TryPeek(reader, (IReadOnlyCollection<char>) expected);
        }

        /// <summary>
        /// Tries to peek to see if the next character is one of the expected characters.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="expected">The expected characters.</param>
        /// <returns><see langword="true"/> if the next character is one of the expected characters;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool TryPeek(this TextReader reader, IReadOnlyCollection<char> expected)
        {
            #region Contract
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            #endregion

            return TryPeek(reader, expected.Contains);
        }

        /// <summary>
        /// Tries to peek to see if the predicate is true for the next character.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns><see langword="true"/> if the predicate is true for the next character;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool TryPeek(this TextReader reader, Func<char, bool> predicate)
        {
            #region Contract
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            #endregion

            int chi = reader.Peek();
            return chi != -1 && predicate((char)chi);
        }
    }
}
