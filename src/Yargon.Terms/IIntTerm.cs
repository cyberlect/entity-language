using System;
using System.Collections.Generic;
using System.Text;

namespace Yargon.Terms
{
    /// <summary>
    /// An integer term.
    /// </summary>
    public interface IIntTerm : ITerm
    {
        /// <summary>
        /// Gets the value of the term.
        /// </summary>
        /// <value>The value.</value>
        int Value { get; }
    }
}
