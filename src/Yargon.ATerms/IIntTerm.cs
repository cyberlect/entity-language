using System.Collections.Generic;

namespace Yargon.ATerms
{
	/// <summary>
	/// An integer number term.
	/// </summary>
	public interface IIntTerm : ITerm
	{
		/// <summary>
		/// Gets the integer value of the term.
		/// </summary>
		/// <value>The value.</value>
		int Value { get; }
	}
}
