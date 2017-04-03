using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Virtlink.Utilib.Collections;

namespace Yargon.Terms.ATerms
{
    /// <summary>
    /// A cache that never caches.
    /// </summary>
    public sealed class NullTermCache : ITermCache
    {
        /// <summary>
        /// Gets a term from the cache that is equal to the specified term;
        /// or the specified term if the cache doesn't contain that term.
        /// </summary>
        /// <typeparam name="T">The type of term to look up.</typeparam>
        /// <param name="term">The term to look up.</param>
        /// <returns>The term from the cache.</returns>
        public T GetOrStore<T>(T term)
            where T : ITerm
        {
            #region Contract
            if (term == null)
                throw new ArgumentNullException(nameof(term));
            #endregion

            return term;
        }
    }
}
