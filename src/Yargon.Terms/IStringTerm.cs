using System;
using System.Collections.Generic;
using System.Text;

namespace Yargon.Terms
{
    /// <summary>
    /// A string term.
    /// </summary>
    public interface IStringTerm : ITerm
    {
        /// <summary>
        /// Gets the value of the term.
        /// </summary>
        /// <value>The value.</value>
        string Value { get; }
    }
}
