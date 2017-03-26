using System;
using System.Collections.Generic;

namespace Yargon.ATerms
{
	/// <summary>
	/// A constructor application term.
	/// </summary>
	public interface IConsTerm : ITerm
	{
		/// <summary>
		/// Gets the name of the constructor.
		/// </summary>
		/// <value>The name of the constructor;
		/// or <see cref="String.Empty"/> when it is a tuple.</value>
		/// <remarks>
		/// When the name is an empty string,
		/// this is the special tuple constructor.
		/// </remarks>
		string Name { get; }

		/// <summary>
		/// Gets whether this is a tuple constructor.
		/// </summary>
		/// <value><see langword="true"/> when it is a tuple constructor;
		/// otherwise, <see langword="false"/>.</value>
		bool IsTuple { get; }
	}
}
