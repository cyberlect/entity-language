using System;
using System.Collections.Generic;
using System.Text;

namespace Yargon.Terms
{
    /// <summary>
    /// A list term.
    /// </summary>
    public interface IListTerm<out T> : ITerm, IReadOnlyList<T>
        where T : class, ITerm
    {
        
    }
}
