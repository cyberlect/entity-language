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
    public sealed class PropertyATerm : Term, IPropertyTerm
    {
        private readonly SubtermCollection subterms;
        /// <inheritdoc />
        public override IReadOnlyList<ITerm> Subterms => this.subterms;

        /// <inheritdoc />
        public IStringTerm Name { get; }

        /// <inheritdoc />
        public ITypeTerm Type { get; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyATerm"/> class.
        /// </summary>
        /// <param name="factory">The factory that created this term.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="annotations">The annotations of the term.</param>
        public PropertyATerm(ITermFactory factory, IStringTerm name, ITypeTerm type, IReadOnlyCollection<ITerm> annotations)
            : base(factory, annotations)
        {
            #region Contract
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            #endregion

            this.Name = name;
            this.Type = type;
            this.subterms = new SubtermCollection(this);
        }
        #endregion

        #region Equality
        /// <inheritdoc />
        public bool Equals(PropertyATerm other)
        {
            if (Object.ReferenceEquals(this, other))
                return true;

            return base.Equals(other)
                   && this.Name.Equals(other.Name)
                   && this.Type.Equals(other.Type);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hash = 17;
            unchecked
            {
                hash = hash * 29 + this.Name.GetHashCode();
                hash = hash * 29 + this.Type.GetHashCode();
            }
            return hash;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => Equals(obj as PropertyATerm);
        #endregion

        /// <inheritdoc />
        public override string ToString()
            => $"Module({this.Name}, {this.Type})";

        /// <summary>
        /// A collection of subterms.
        /// </summary>
        private struct SubtermCollection : IReadOnlyList<ITerm>
        {
            private readonly PropertyATerm owner;
            
            /// <inheritdoc />
            public int Count => 2;

            /// <inheritdoc />
            public ITerm this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return this.owner.Name;
                        case 1: return this.owner.Type;
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
            public SubtermCollection(PropertyATerm owner)
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
                yield return this[1];
            }

            /// <inheritdoc />
            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }
    }
}
