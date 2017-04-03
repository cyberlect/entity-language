using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Virtlink.Utilib.Collections;

namespace Yargon.Terms.ATerms
{
    /// <summary>
    /// A term.
    /// </summary>
    public abstract class Term : ITerm
    {
        /// <inheritdoc />
        public ITermFactory Factory { get; }

        /// <inheritdoc />
        public abstract IReadOnlyList<ITerm> Subterms { get; }

        /// <inheritdoc />
        public IReadOnlyCollection<ITerm> Annotations { get; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Term"/> class.
        /// </summary>
        /// <param name="factory">The factory that created this term.</param>
        /// <param name="annotations">The annotations of the term.</param>
        protected Term(ITermFactory factory, IReadOnlyCollection<ITerm> annotations)
        {
            #region Contract
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));
            if (annotations == null)
                throw new ArgumentNullException(nameof(annotations));
            #endregion

            this.Factory = factory;
            this.Annotations = annotations;
        }
        #endregion

        #region Equality
        /// <inheritdoc />
        public override int GetHashCode()
        {
            // NOTE: We don't use the annotations in the hash code
            // calculation for performance reasons.
            return 17;
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            return this.Equals(other as Term);
        }

        /// <summary>
        /// Determines whether this object is equal to another object.
        /// </summary>
        /// <param name="other">The other object.</param>
        /// <returns><see langword="true"/> when the objects are equal;
        /// otherwise, <see langword="false"/>.</returns>
        private bool Equals(Term other)
        {
            if (Object.ReferenceEquals(this, other))
                return true;

            return !Object.ReferenceEquals(other, null)
                && other.GetType() == this.GetType()
                && this.Annotations.SequenceEqual(other.Annotations);
        }
        #endregion

        /// <inheritdoc />
        public abstract string ToString();
    }
}
