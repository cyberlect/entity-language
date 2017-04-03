using System;
using System.Collections.Generic;
using System.Linq;

namespace Yargon.Terms
{
    /// <summary>
    /// Extension methods for working with term factories.
    /// </summary>
    public static class TermFactoryExtensions
    {
        /// <summary>
        /// Creates a new term of the specified type, with the specified subterms and annotations.
        /// </summary>
        /// <param name="termFactory">The term factory.</param>
        /// <param name="type">The type of term.</param>
        /// <param name="subterms">The subterms.</param>
        /// <returns>The created term.</returns>
        public static ITerm Create(this ITermFactory termFactory, Type type, IEnumerable<ITerm> subterms)
        {
            #region Contract
            if (termFactory == null)
                throw new ArgumentNullException(nameof(termFactory));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (subterms == null)
                throw new ArgumentNullException(nameof(subterms));
            #endregion

            return termFactory.Create(type, subterms, Enumerable.Empty<ITerm>());
        }

        /// <summary>
        /// Creates a new term of the specified type, with the specified subterms and annotations.
        /// </summary>
        /// <param name="termFactory">The term factory.</param>
        /// <param name="type">The type of term.</param>
        /// <param name="subterms">The subterms.</param>
        /// <returns>The created term.</returns>
        public static ITerm Create(this ITermFactory termFactory, Type type, params ITerm[] subterms)
            => termFactory.Create(type, (IEnumerable<ITerm>)subterms);

        /// <summary>
        /// Creates a new term of the specified type, with the specified subterms and annotations.
        /// </summary>
        /// <param name="termFactory">The term factory.</param>
        /// <param name="subterms">The subterms.</param>
        /// <param name="annotations">The annotations.</param>
        /// <returns>The created term.</returns>
        public static T Create<T>(this ITermFactory termFactory, IEnumerable<ITerm> subterms, IEnumerable<ITerm> annotations)
        {
            #region Contract
            if (termFactory == null)
                throw new ArgumentNullException(nameof(termFactory));
            if (subterms == null)
                throw new ArgumentNullException(nameof(subterms));
            if (annotations == null)
                throw new ArgumentNullException(nameof(annotations));
            #endregion

            return (T)termFactory.Create(typeof(T), subterms, annotations);
        }

        /// <summary>
        /// Creates a new term of the specified type, with the specified subterms and annotations.
        /// </summary>
        /// <param name="termFactory">The term factory.</param>
        /// <param name="subterms">The subterms.</param>
        /// <returns>The created term.</returns>
        public static T Create<T>(this ITermFactory termFactory, IEnumerable<ITerm> subterms)
        {
            #region Contract
            if (termFactory == null)
                throw new ArgumentNullException(nameof(termFactory));
            if (subterms == null)
                throw new ArgumentNullException(nameof(subterms));
            #endregion

            return termFactory.Create<T>(subterms, Enumerable.Empty<ITerm>());
        }

        /// <summary>
        /// Creates a new term of the specified type, with the specified subterms and annotations.
        /// </summary>
        /// <param name="termFactory">The term factory.</param>
        /// <param name="subterms">The subterms.</param>
        /// <returns>The created term.</returns>
        public static T Create<T>(this ITermFactory termFactory, params ITerm[] subterms)
            => termFactory.Create<T>((IEnumerable<ITerm>)subterms);


        /// <summary>
        /// Builds a new integer term.
        /// </summary>
        /// <param name="termFactory">The term factory.</param>
        /// <param name="value">The value of the term.</param>
        /// <returns>The built term.</returns>
        public static IIntTerm Int(this ITermFactory termFactory, int value)
        {
            #region Contract
            if (termFactory == null)
                throw new ArgumentNullException(nameof(termFactory));
            #endregion

            return termFactory.Int(value, Enumerable.Empty<ITerm>());
        }

        /// <summary>
        /// Builds a new string term.
        /// </summary>
        /// <param name="termFactory">The term factory.</param>
        /// <param name="value">The value of the term.</param>
        /// <returns>The string term.</returns>
        public static IStringTerm String(this ITermFactory termFactory, string value)
        {
            #region Contract
            if (termFactory == null)
                throw new ArgumentNullException(nameof(termFactory));
            #endregion

            return termFactory.String(value, Enumerable.Empty<ITerm>());
        }

        /// <summary>
        /// Builds a new list term.
        /// </summary>
        /// <param name="termFactory">The term factory.</param>
        /// <param name="terms">The terms in the list.</param>
        /// <returns>The list term.</returns>
        public static IListTerm<T> List<T>(this ITermFactory termFactory, IEnumerable<T> terms)
            where T : class, ITerm
        {
            #region Contract
            if (termFactory == null)
                throw new ArgumentNullException(nameof(termFactory));
            if (terms == null)
                throw new ArgumentNullException(nameof(terms));
            #endregion

            return termFactory.List(terms, Enumerable.Empty<ITerm>());
        }


        /// <summary>
        /// Builds a new list term.
        /// </summary>
        /// <param name="termFactory">The term factory.</param>
        /// <param name="terms">The terms in the list.</param>
        /// <returns>The list term.</returns>
        public static IListTerm<T> List<T>(this ITermFactory termFactory, params T[] terms)
            where T : class, ITerm
            => termFactory.List((IEnumerable<T>)terms);
    }
}
