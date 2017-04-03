using System;
using System.Collections.Generic;

namespace Yargon.Terms
{
    /// <summary>
    /// A term.
    /// </summary>
    public interface ITerm : ITermImpl
    {
        /// <summary>
		/// Gets the subterms of the term.
		/// </summary>
		/// <value>A list of subterms of the term.</value>
		IReadOnlyList<ITerm> Subterms { get; }

        /// <summary>
        /// Gets a set of annotations of the term.
        /// </summary>
        /// <value>A set of annotations of the term.</value>
        IReadOnlyCollection<ITerm> Annotations { get; }
    }
}
