using System;
using System.Collections.Generic;
using System.Text;
using Yargon.Terms;

namespace EntityLanguage
{
    /// <summary>
    /// A property.
    /// </summary>
    public interface IPropertyTerm : ITerm
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        IStringTerm Name { get; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        ITypeTerm Type { get; }
    }
}
