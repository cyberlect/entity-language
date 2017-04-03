using System;
using System.Collections.Generic;
using System.Text;
using Yargon.Terms;

namespace EntityLanguage
{
    /// <summary>
    /// A module.
    /// </summary>
    public interface IModuleTerm : ITerm
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        IStringTerm Name { get; }

        /// <summary>
        /// Gets the definitions.
        /// </summary>
        /// <value>The definitions.</value>
        IListTerm<IEntityTerm> Definitions { get; }
    }
}
