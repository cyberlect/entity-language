using System;
using System.Collections.Generic;
using System.Text;
using Virtlink.Utilib.Collections;

namespace Yargon.ATerms
{
    /// <summary>
    /// Extension methods and functions for working with terms.
    /// </summary>
    public static class Terms
    {
        /// <summary>
	    /// Gets an empty list of terms.
	    /// </summary>
	    /// <value>An empty list of terms.</value>
	    public static IReadOnlyList<ITerm> Empty { get; } = List.Empty<ITerm>();
    }
}
