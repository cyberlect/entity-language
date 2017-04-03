using System;
using System.Collections.Generic;
using System.Text;

namespace Yargon.Terms
{
    /// <summary>
    /// A term factory.
    /// </summary>
    public interface ITermFactory
    {
        /// <summary>
        /// Creates a new term of the specified type, with the specified subterms and annotations.
        /// </summary>
        /// <param name="type">The type of term.</param>
        /// <param name="subterms">The subterms.</param>
        /// <param name="annotations">The annotations.</param>
        /// <returns>The created term.</returns>
        ITerm Create(Type type, IEnumerable<ITerm> subterms, IEnumerable<ITerm> annotations);

        /// <summary>
        /// Builds a new integer term.
        /// </summary>
        /// <param name="value">The value of the term.</param>
        /// <param name="annotations">A set of annotations for the term.</param>
        /// <returns>The built term.</returns>
        IIntTerm Int(int value, IEnumerable<ITerm> annotations);

        /// <summary>
        /// Builds a new string term.
        /// </summary>
        /// <param name="value">The value of the term.</param>
        /// <param name="annotations">A set of annotations for the term.</param>
        /// <returns>The string term.</returns>
        IStringTerm String(string value, IEnumerable<ITerm> annotations);

        /// <summary>
        /// Builds a new list term.
        /// </summary>
        /// <param name="elements">The elements in the list.</param>
        /// <param name="annotations">A set of annotations for the term.</param>
        /// <returns>The list term.</returns>
        IListTerm<T> List<T>(IEnumerable<T> elements, IEnumerable<ITerm> annotations)
            where T : class, ITerm;
    }
}
