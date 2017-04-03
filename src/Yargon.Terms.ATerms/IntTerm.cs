using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Virtlink.Utilib.Collections;

namespace Yargon.Terms.ATerms
{
    /// <summary>
    /// An integer term.
    /// </summary>
    public sealed class IntTerm : Term, IIntTerm
    {
        /// <inheritdoc />
        public override IReadOnlyList<ITerm> Subterms => List.Empty<ITerm>();

        /// <summary>
        /// Gets the value of the term.
        /// </summary>
        /// <value>The value.</value>
        public int Value { get; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="IntTerm"/> class.
        /// </summary>
        /// <param name="factory">The factory that created this term.</param>
        /// <param name="value">The value.</param>
        /// <param name="annotations">The annotations of the term.</param>
        public IntTerm(ITermFactory factory, int value, IReadOnlyCollection<ITerm> annotations)
            : base(factory, annotations)
        {
            this.Value = value;
        }
        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
