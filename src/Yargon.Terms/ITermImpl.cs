using System;
using System.Collections.Generic;
using System.Text;

namespace Yargon.Terms
{
    /// <summary>
    /// A term implementation.
    /// </summary>
    public interface ITermImpl
    {
        /// <summary>
        /// Gets the factory that created the term.
        /// </summary>
        ITermFactory Factory { get; }
    }
}
