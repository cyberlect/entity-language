using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Virtlink.Utilib.Collections;

namespace Yargon.Terms.ATerms
{
    /// <summary>
    /// A string term.
    /// </summary>
    public sealed class StringTerm : Term, IStringTerm
    {
        /// <inheritdoc />
        public override IReadOnlyList<ITerm> Subterms => List.Empty<ITerm>();

        /// <summary>
        /// Gets the value of the term.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StringTerm"/> class.
        /// </summary>
        /// <param name="factory">The factory that created this term.</param>
        /// <param name="value">The value.</param>
        /// <param name="annotations">The annotations of the term.</param>
        public StringTerm(ITermFactory factory, string value, IReadOnlyCollection<ITerm> annotations)
            : base(factory, annotations)
        {
            #region Contract
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            #endregion

            this.Value = value;
        }
        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return $"\"{this.Value}\"";
        }
    }
}
