using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Yargon.Terms;
using Yargon.Terms.ATerms;

namespace EntityLanguage
{
    /// <summary>
    /// A property.
    /// </summary>
    public sealed class TypeATerm : Term, ITypeTerm
    {
        private readonly SubtermCollection subterms;
        /// <inheritdoc />
        public override IReadOnlyList<ITerm> Subterms => this.subterms;

        /// <inheritdoc />
        public IStringTerm Name { get; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeATerm"/> class.
        /// </summary>
        /// <param name="factory">The factory that created this term.</param>
        /// <param name="name">The name.</param>
        /// <param name="annotations">The annotations of the term.</param>
        public TypeATerm(ITermFactory factory, IStringTerm name, IReadOnlyCollection<ITerm> annotations)
            : base(factory, annotations)
        {
            #region Contract
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            #endregion

            this.Name = name;
            this.subterms = new SubtermCollection(this);
        }
        #endregion

        #region Equality
        /// <inheritdoc />
        public bool Equals(TypeATerm other)
        {
            if (Object.ReferenceEquals(this, other))
                return true;

            return base.Equals(other)
                   && this.Name.Equals(other.Name);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hash = 17;
            unchecked
            {
                hash = hash * 29 + this.Name.GetHashCode();
            }
            return hash;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => Equals(obj as TypeATerm);
        #endregion

        /// <inheritdoc />
        public override string ToString()
            => $"Module({this.Name})";

        /// <summary>
        /// A collection of subterms.
        /// </summary>
        private struct SubtermCollection : IReadOnlyList<ITerm>
        {
            private readonly TypeATerm owner;
            
            /// <inheritdoc />
            public int Count => 1;

            /// <inheritdoc />
            public ITerm this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return this.owner.Name;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(index));
                    }
                }
            }

            #region Constructors
            /// <summary>
            /// Initializes a new instance of the <see cref="SubtermCollection"/> class.
            /// </summary>
            /// <param name="owner">The owner of this collection.</param>
            public SubtermCollection(TypeATerm owner)
            {
                #region Contract
                Debug.Assert(owner != null);
                #endregion

                this.owner = owner;
            }
            #endregion

            /// <inheritdoc />
            public IEnumerator<ITerm> GetEnumerator()
            {
                yield return this[0];
            }

            /// <inheritdoc />
            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }
    }
}
