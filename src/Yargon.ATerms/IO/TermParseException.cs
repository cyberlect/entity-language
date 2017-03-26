﻿using System;

namespace Yargon.ATerms.IO
{
	/// <summary>
	/// A term parsing exception has occurred.
	/// </summary>
	public class TermParseException : InvalidOperationException
	{
		private const string DefaultMessage = "A term parsing error.";

		/// <summary>
		/// Initializes a new instance of the <see cref="TermParseException"/> class.
		/// </summary>
		public TermParseException()
			: this(null, null)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="TermParseException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public TermParseException(string message)
			: this(message, null)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="TermParseException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="inner">The inner exception.</param>
		public TermParseException(string message, Exception inner)
			: base(message ?? TermParseException.DefaultMessage, inner)
		{ }
	}
}
