using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using Virtlink.Utilib;

namespace Yargon.ATerms.IO
{
	/// <summary>
	/// Reads terms from a texual format.
	/// </summary>
	public class ATermReader : TermTextReader
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ATermReader"/> class.
		/// </summary>
		/// <param name="termFactory">The term factory to use.</param>
		internal ATermReader(ITermFactory termFactory)
			: this(termFactory, TermTextReader.DefaultCulture)
        {
            // Nothing to do.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ATermReader"/> class.
        /// </summary>
        /// <param name="termFactory">The term factory to use.</param>
        /// <param name="culture">The culture of the reader.</param>
        internal ATermReader(ITermFactory termFactory, CultureInfo culture)
			: base(termFactory, culture)
        {
            #region Contract
            if (termFactory == null)
                throw new ArgumentNullException(nameof(termFactory));
            if (culture == null)
                throw new ArgumentNullException(nameof(culture));
            #endregion
		}
		#endregion

		/// <inheritdoc />
		public override ITerm Read(TextReader reader)
        {
            #region Contract
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            #endregion

            if (!ReadWhitespace(reader))
            {
                // Read until the end.
                return null;
            }

            char ch = reader.PeekOrFail();

			switch (ch)
			{
				case '[': return ReadList(reader);
				case '(': return ReadTuple(reader);
				case '"': return ReadString(reader);
				case '<': return ReadPlaceholder(reader);
			}

			if (Char.IsNumber(ch))
				return ReadNumber(reader);
			else if (Char.IsLetter(ch))
				return ReadCons(reader);
			
			throw new TermParseException($"Invalid term starting with '{ch}'.");
		}

		/// <summary>
		/// Reads a list.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <remarks>
		/// The reader must be positioned at the opening `[` character
		/// of the list, and will be positioned at the character following the closing `]`
		/// character of the string or the closing `}` bracket of its annotations (if any).
		/// </remarks>
		private IListTerm ReadList(TextReader reader)
		{
			#region Contract
			Debug.Assert(reader != null);
			#endregion

		    reader.ReadExpected('[', "list");

			var terms = ReadTermSequence(reader, ',', ']');
			var annotations = ReadAnnotations(reader);

			return this.TermFactory.List(terms, annotations);
		}

		/// <summary>
		/// Reads a tuple.
		/// </summary>
		/// <param name="reader">The reader.</param>
		private IConsTerm ReadTuple(TextReader reader)
		{
			#region Contract
			Debug.Assert(reader != null);
			#endregion

			return ReadCons(reader);
		}

		/// <summary>
		/// Reads a string.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <remarks>
		/// The reader must be positioned at the opening `"` character
		/// of the string, and will be positioned at the character following the closing `"`
		/// character of the string or the closing `}` bracket of its annotations (if any).
		/// </remarks>
		private IStringTerm ReadString(TextReader reader)
		{
			#region Contract
			Debug.Assert(reader != null);
			#endregion

		    reader.ReadExpected('"', "string");

			var stringBuilder = new StringBuilder();
            
		    char ch = reader.ReadOrFail();

			while (ch != '"')
			{
				if (ch == '\\')
				{
					// Escaped character.
				    ch = reader.ReadOrFail();

				    switch (ch)
				    {
				        case 'n': // Line feed
				            stringBuilder.Append('\n');
				            break;
				        case 'r': // Carriage return
				            stringBuilder.Append('\r');
				            break;
				        case 'f': // Form feed
				            stringBuilder.Append('\f');
				            break;
				        case 't': // Horizontal tab
				            stringBuilder.Append('\t');
				            break;
				        case '\\': // Backslash
				            stringBuilder.Append('\\');
				            break;
				        case '\'': // Single quote
				            stringBuilder.Append('\'');
				            break;
				        case '"': // Double quote
				            stringBuilder.Append('"');
				            break;
				        case 'u': // 4-digit decimal Unicode character
				            stringBuilder.Append(ReadEscapedDecimalUnicode(reader, 4, 4));
				            break;
				        case 'U': // 8-digit decimal Unicode character
				            stringBuilder.Append(ReadEscapedDecimalUnicode(reader, 8, 8));
				            break;
				        case 'x': // up to 4 digit hexadecimal Unicode character
				            stringBuilder.Append(ReadEscapedHexadecimalUnicode(reader, 1, 4));
				            break;
				        default:
				            throw new TermParseException($"Unrecognized escape sequence: '\\{ch}'.");
				    }
				}
				else
				{
					// Any non-escaped character.
					stringBuilder.Append(ch);
				}
                
				ch = reader.ReadOrFail();
			}

			var annotations = ReadAnnotations(reader);

			return this.TermFactory.String(stringBuilder.ToString(), annotations);
		}

	    /// <summary>
	    /// Reads a sequence of decimal digits into an Unicode character.
	    /// </summary>
	    /// <param name="reader">The reader to read from.</param>
	    /// <param name="minLength">The minimum number of digits to read.</param>
	    /// <param name="maxLength">The maximum number of digits to read.</param>
	    /// <returns>The Unicode characters that were read.</returns>
	    private string ReadEscapedDecimalUnicode(TextReader reader, int minLength, int maxLength)
	    {
	        return ReadEscapedUnicode(reader, Char.IsDigit, NumberStyles.Integer, minLength, maxLength);
	    }

	    /// <summary>
        /// Reads a sequence of hexadecimal digits into an Unicode character.
        /// </summary>
        /// <param name="reader">The reader to read from.</param>
        /// <param name="minLength">The minimum number of digits to read.</param>
        /// <param name="maxLength">The maximum number of digits to read.</param>
        /// <returns>The Unicode characters that were read.</returns>
        private string ReadEscapedHexadecimalUnicode(TextReader reader, int minLength, int maxLength)
	    {
	        return ReadEscapedUnicode(reader, Chars.IsHexDigit, NumberStyles.HexNumber, minLength, maxLength);
	    }

        /// <summary>
        /// Reads a sequence of digits into an Unicode character.
        /// </summary>
        /// <param name="reader">The reader to read from.</param>
        /// <param name="validator">Character validator function.</param>
        /// <param name="numberStyle">The number style used to parse.</param>
        /// <param name="minLength">The minimum number of digits to read.</param>
        /// <param name="maxLength">The maximum number of digits to read.</param>
        /// <returns>The Unicode characters that were read.</returns>
        private string ReadEscapedUnicode(TextReader reader, Func<char, bool> validator, NumberStyles numberStyle, int minLength, int maxLength)
	    {
	        #region Contract
	        Debug.Assert(reader != null);
	        Debug.Assert(minLength >= 0 && minLength < 8);
	        Debug.Assert(maxLength > 0 && maxLength <= 8);
	        #endregion

            var sb = new StringBuilder(maxLength);
	        for (int i = 0; i < maxLength; i++)
	        {
	            char c = reader.PeekOrFail();
	            if (!validator(c))
	                break;
	            sb.Append(c);
	        }
            if (sb.Length < minLength)
                throw new TermParseException($"Expected at least {minLength} digits to form a Unicode character escape sequence, found {sb.Length}.");

	        int result;
	        if (!Int32.TryParse(sb.ToString(), numberStyle, CultureInfo.InvariantCulture, out result))
                throw new TermParseException($"Digits don't form a valid Unicode character escape sequence.");
	        return Char.ConvertFromUtf32(result);
	    }

		/// <summary>
		/// Reads a number.
		/// </summary>
		/// <param name="reader">The reader.</param>
		private ITerm ReadNumber(TextReader reader)
		{
			#region Contract
			Debug.Assert(reader != null);
			#endregion

			string ints = ReadDigits(reader);
			string frac = String.Empty;
			string exp = String.Empty;

		    if (reader.TryPeek('.'))
		    {
		        reader.ReadExpected('.');

		        frac = ReadDigits(reader);

		        if (reader.TryPeek('e', 'E'))
		        {
		            reader.ReadExpected('e', 'E');

		            exp = ReadDigits(reader);
		        }
		    }

		    if (ints == String.Empty && frac == String.Empty)
				throw new TermParseException("Expected number, got something else.");

			var annotations = ReadAnnotations(reader);

			if (frac != String.Empty)
			{
				double value = Double.Parse(
					(!String.IsNullOrWhiteSpace(ints) ? ints : "0") + "." +
					(!String.IsNullOrWhiteSpace(frac) ? frac : "0") + "e" +
					(!String.IsNullOrWhiteSpace(exp) ? exp : "0"),
					CultureInfo.InvariantCulture);
				// TODO: Support doubles instead of floats.
				return this.TermFactory.Real((float)value, annotations);
			}
			else
			{
				int value = Int32.Parse(ints, CultureInfo.InvariantCulture);
				return this.TermFactory.Int(value);
			}
		}

		/// <summary>
		/// Reads a placeholder.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <remarks>
		/// The reader must be positioned at the opening `&lt;` character
		/// of the placeholder, and will be positioned at the character following the closing `&gt;`
		/// character of the placeholder or the closing `}` bracket of its annotations (if any).
		/// </remarks>
		private IPlaceholderTerm ReadPlaceholder(TextReader reader)
		{
			#region Contract
			Debug.Assert(reader != null);
            #endregion

		    reader.ReadExpected('<', "placeholder start");

			var template = Read(reader);

			ReadWhitespace(reader);
			
		    reader.ReadExpected('>', "placeholder end");

			var annotations = ReadAnnotations(reader);

			return this.TermFactory.Placeholder(template, annotations);
		}

		/// <summary>
		/// Reads an application (or tuple).
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <remarks>
		/// The reader must be positioned at the first character of the application or tuple,
		/// and will be positioned at the character following the the application/tuple
		/// or the closing `}` bracket of its annotations (if any).
		/// </remarks>
		private IConsTerm ReadCons(TextReader reader)
		{
			#region Contract
			Debug.Assert(reader != null);
			#endregion

			string name = ReadIdentifier(reader);

			ReadWhitespace(reader);
            
			char ch = reader.PeekOrFail();
			IReadOnlyList<ITerm> terms;
			if (ch == '(')
			{
				reader.ReadExpected('(');
				terms = ReadTermSequence(reader, ',', ')');
			}
			else
			{
				terms = Terms.Empty;
			}

			var annotations = ReadAnnotations(reader);

			return this.TermFactory.Cons(name, terms, annotations);
		}

		/// <summary>
		/// Reads annotations.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns>A list of annotations.</returns>
		private IReadOnlyList<ITerm> ReadAnnotations(TextReader reader)
		{
			#region Contract
			Debug.Assert(reader != null);
			#endregion
            
            if (!reader.TryPeek('{'))
				// No annotations to read.
				return Terms.Empty;
            
			reader.ReadExpected('{');

			return ReadTermSequence(reader, ',', '}');
		}

		/// <summary>
		/// Reads an identifier.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns>The read identifier, which may be an empty string.</returns>
		/// <remarks>
		/// The reader must be positioned at the first character of the identifier,
		/// and will be positioned at the first non-identifier character following the identifier.
		/// </remarks>
		private string ReadIdentifier(TextReader reader)
		{
			#region Contract
			Debug.Assert(reader != null);
			#endregion

			StringBuilder stringBuilder = new StringBuilder();
            
			char ch = reader.PeekOrFail();
			if (ATermReader.IsValidIdentifierFirstChar(ch))
			{
				ch = reader.ReadOrFail();
				stringBuilder.Append(ch);

                while (reader.TryPeek(IsValidIdentifierChar))
				{
					ch = reader.ReadOrFail();
					stringBuilder.Append(ch);
				}
			}

			return stringBuilder.ToString();
		}

		/// <summary>
		/// Reads a sequence of digits.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns>The read digits, which may be an empty string.</returns>
		/// <remarks>
		/// The reader must be positioned at the first digit,
		/// and will be positioned at the first non-digit character following the digits.
		/// </remarks>
		private string ReadDigits(TextReader reader)
		{
			#region Contract
			Debug.Assert(reader != null);
			#endregion

			StringBuilder stringBuilder = new StringBuilder();
            
			char ch = reader.PeekOrFail();
			while (Char.IsDigit(ch))
			{
				ch = reader.ReadOrFail();
				stringBuilder.Append(ch);
                
				int chi = reader.Peek();
			    if (chi == -1)
			        break;
			    ch = (char) chi;
			}

			return stringBuilder.ToString();
		}

		/// <summary>
		/// Returns whether the specified character is valid as the first character
		/// of an identifier.
		/// </summary>
		/// <param name="ch">The character to check.</param>
		/// <returns><see langword="true"/> when it is valid;
		/// otherwise, <see langword="false"/>.</returns>
		[Pure]
		private static bool IsValidIdentifierFirstChar(char ch)
		{
			return Char.IsLetter(ch)
				|| ch == '_'
				|| ch == '-'
				|| ch == '+'
				|| ch == '*'
				|| ch == '$';
		}

		/// <summary>
		/// Returns whether the specified character is valid as a subsequent character
		/// of an identifier.
		/// </summary>
		/// <param name="ch">The character to check.</param>
		/// <returns><see langword="true"/> when it is valid;
		/// otherwise, <see langword="false"/>.</returns>
		[Pure]
		private static bool IsValidIdentifierChar(char ch)
		{
			return Char.IsLetterOrDigit(ch)
				|| ch == '_'
				|| ch == '-'
				|| ch == '+'
				|| ch == '*'
				|| ch == '$';
		}

		/// <summary>
		/// Reads a sequence of terms.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="separator">The separator character.</param>
		/// <param name="end">The end character.</param>
		/// <returns>The read terms.</returns>
		/// <remarks>
		/// The <paramref name="end"/> character is also consumed.
		/// </remarks>
		private IReadOnlyList<ITerm> ReadTermSequence(TextReader reader, char separator, char end)
		{
			#region Contract
			Debug.Assert(reader != null);
			#endregion
			
			ReadWhitespace(reader);
            
			char ch = reader.PeekOrFail();
			if (ch == end)
			{
				reader.ReadExpected(end);
				return Terms.Empty;
			}

			var terms = new List<ITerm>();
			do
			{
				var term = Read(reader);
				terms.Add(term);

				ReadWhitespace(reader);
				ch = reader.ReadOrFail();
			} while (ch == separator);

			if (ch != end)
				throw new TermParseException($"Term sequence didn't end with '{end}': '{ch}'.");

			return terms;
		}

        /// <summary>
        /// Reads whitespace.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns><see langword="true"/> when there may be more to read;
        /// otherwise, <see langword="false"/> when the end of the stream has been reached.</returns>
        private bool ReadWhitespace(TextReader reader)
		{
			#region Contract
			Debug.Assert(reader != null);
			#endregion

		    int ch = reader.Peek();
		    if (ch == -1) return false;

		    while (Char.IsWhiteSpace((char) ch))
		    {
		        // Consume the character.
		        reader.Read();

                // Peek the next character.
		        ch = reader.Peek();
		        if (ch == -1) return false;
		    }

		    return true;
		}
	}
}
