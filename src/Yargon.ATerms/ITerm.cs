using System;
using System.Collections.Generic;

namespace Yargon.ATerms
{
	/// <summary>
	/// An immutable term.
	/// </summary>
	public interface ITerm
	{
        /// <summary>
        /// Gets the factory that created this term.
        /// </summary>
        /// <value>The term factory.</value>
        ITermFactory Factory { get; }

		/// <summary>
		/// Gets the sub terms of the term.
		/// </summary>
		/// <value>A list of sub terms of the term.</value>
		IReadOnlyList<ITerm> SubTerms { get; }

		/// <summary>
		/// Gets a set of annotations of the term.
		/// </summary>
		/// <value>A set of annotations of the term.</value>
		IReadOnlyCollection<ITerm> Annotations { get; }

		/// <summary>
		/// Gets a sub term by index.
		/// </summary>
		/// <param name="index">The zero-based index of the subterm.</param>
		/// <returns>The subterm.</returns>
		ITerm this[int index] { get; }

		/// <summary>
		/// Accepts the term visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		void Accept(ITermVisitor visitor);

        /// <summary>
        /// Accepts the term visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <returns>The result of visiting the term.</returns>
        TResult Accept<TResult>(ITermVisitor<TResult> visitor);
	}
}
