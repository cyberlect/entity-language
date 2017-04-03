using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Yargon.Terms.ATerms
{
    /// <summary>
    /// A list term.
    /// </summary>
    public sealed class ListTerm<T> : Term, IListTerm<T>
        where T : class, ITerm
    {
        /// <summary>
        /// Gets the elements in the list.
        /// </summary>
        /// <value>The elements.</value>
        private IReadOnlyList<T> Elements { get; }

        /// <inheritdoc />
        public override IReadOnlyList<ITerm> Subterms => this.Elements;

        /// <inheritdoc />
        public T this[int index] => throw new NotImplementedException();

        /// <inheritdoc />
        public int Count => throw new NotImplementedException();

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ListTerm{T}"/> class.
        /// </summary>
        /// <param name="factory">The factory that created this term.</param>
        /// <param name="elements">The elements.</param>
        /// <param name="annotations">The annotations of the term.</param>
        public ListTerm(ITermFactory factory, IReadOnlyList<T> elements, IReadOnlyCollection<ITerm> annotations)
            : base(factory, annotations)
        {
            #region Contract
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));
            #endregion

            this.Elements = elements;
        }
        #endregion

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return this.Elements.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{String.Join(", ", this.Subterms)}]";
        }
    }
}
