using System.Collections.Generic;

namespace Yargon.ATerms
{
	/// <summary>
	/// A placeholder term.
	/// </summary>
	public interface IPlaceholderTerm : ITerm
	{
		/// <summary>
		/// Gets the template of the placeholder.
		/// </summary>
		/// <value>The template <see cref="ITerm"/>.</value>
		ITerm Template { get; }
	}
}
