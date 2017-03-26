using System;
using System.Collections.Generic;
using System.Linq;
using Yargon.ATerms.IO;

namespace Yargon.ATerms
{
	partial class TrivialTermFactory
	{
		/// <summary>
		/// A term.
		/// </summary>
		public abstract class Term : ITerm, IEquatable<ITerm>
        {
            /// <inheritdoc />
            public ITermFactory Factory { get; }

            /// <inheritdoc />
            public virtual IReadOnlyList<ITerm> SubTerms { get; } = Terms.Empty;

			/// <inheritdoc />
			public ITerm this[int index] => this.SubTerms[index];

		    /// <inheritdoc />
			public IReadOnlyCollection<ITerm> Annotations { get; }

            #region Constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="Term"/> class.
            /// </summary>
            /// <param name="termFactory">The term factory that created this term.</param>
            /// <param name="annotations">The annotations of the term.</param>
            protected Term(ITermFactory termFactory, IReadOnlyCollection<ITerm> annotations)
            {
                #region Contract
                if (termFactory == null)
                    throw new ArgumentNullException(nameof(annotations));
                if (annotations == null)
                    throw new ArgumentNullException(nameof(annotations));
                #endregion

                this.Factory = termFactory;
                this.Annotations = annotations;
			}
			#endregion

			#region Equality
			/// <inheritdoc />
			public override int GetHashCode()
			{
                int hash = 17;
                unchecked
                {
                    hash = hash * 29 + this.Factory.GetHashCode();
                    // NOTE: We don't use the annotations in the hash code
                    // calculation for performance reasons.
                }
                return hash;
            }

			/// <inheritdoc />
			public override bool Equals(object other) => this.Equals(other as ITerm);

			/// <summary>
			/// Determines whether this object is equal to another object.
			/// </summary>
			/// <param name="other">The other object.</param>
			/// <returns><see langword="true"/> when the objects are equal;
			/// otherwise, <see langword="false"/>.</returns>
			public bool Equals(ITerm other)
			{
			    if (Object.ReferenceEquals(this, other))
			        return true;
                // When 'other' is null or of a different type
                // then they are not equal.
                if (Object.ReferenceEquals(other, null) ||
					other.GetType() != this.GetType())
					return false;
                return Object.Equals(this.Factory, other.Factory)
                    && this.Annotations.SequenceEqual(other.Annotations);
			}
			#endregion

		    /// <inheritdoc />
		    public virtual void Accept(ITermVisitor visitor)
		    {
		        #region Contract
		        if (visitor == null)
		            throw new ArgumentNullException(nameof(visitor));
		        #endregion

		        visitor.VisitTerm(this);
		    }

		    /// <inheritdoc />
		    public virtual TResult Accept<TResult>(ITermVisitor<TResult> visitor)
		    {
		        #region Contract
		        if (visitor == null)
		            throw new ArgumentNullException(nameof(visitor));
		        #endregion

		        return visitor.VisitTerm(this);
		    }

		    /// <inheritdoc />
            public override string ToString()
			{
				return ATermFormat.Instance.CreateWriter().ToString(this);
			}
		}
	}
}
