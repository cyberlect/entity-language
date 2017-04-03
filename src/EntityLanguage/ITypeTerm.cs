using System;
using System.Collections.Generic;
using System.Text;
using Yargon.Terms;

namespace EntityLanguage
{
    /// <summary>
    /// A type.
    /// </summary>
    public interface ITypeTerm : ITerm
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        IStringTerm Name { get; }
    }
}
