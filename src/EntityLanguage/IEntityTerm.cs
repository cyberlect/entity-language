using System;
using System.Collections.Generic;
using System.Text;
using Yargon.Terms;

namespace EntityLanguage
{
    /// <summary>
    /// An entity.
    /// </summary>
    public interface IEntityTerm : ITerm
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        IStringTerm Name { get; }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        IListTerm<IPropertyTerm> Properties { get; }
    }
}
