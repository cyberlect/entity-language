using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Yargon.Terms;
using Yargon.Terms.ATerms;

namespace EntityLanguage
{
    /// <summary>
    /// An entity.
    /// </summary>
    public sealed class EntityATerm : Term, IEntityTerm
    {
        private readonly SubtermCollection subterms;
        /// <inheritdoc />
        public override IReadOnlyList<ITerm> Subterms => this.subterms;

        /// <inheritdoc />
        public IStringTerm Name { get; }

        /// <inheritdoc />
        public IListTerm<IPropertyTerm> Properties { get; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityATerm"/> class.
        /// </summary>
        /// <param name="factory">The factory that created this term.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="annotations">The annotations of the term.</param>
        public EntityATerm(ITermFactory factory, IStringTerm name, IListTerm<IPropertyTerm> properties, IReadOnlyCollection<ITerm> annotations)
            : base(factory, annotations)
        {
            #region Contract
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));
            #endregion

            this.Name = name;
            this.Properties = properties;
            this.subterms = new SubtermCollection(this);
        }
        #endregion

        #region Equality
        /// <inheritdoc />
        public bool Equals(EntityATerm other)
        {
            if (Object.ReferenceEquals(this, other))
                return true;

            return base.Equals(other)
                   && this.Name.Equals(other.Name)
                   && this.Properties.Equals(other.Properties);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hash = 17;
            unchecked
            {
                hash = hash * 29 + this.Name.GetHashCode();
                hash = hash * 29 + this.Properties.GetHashCode();
            }
            return hash;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => Equals(obj as EntityATerm);
        #endregion

        /// <inheritdoc />
        public override string ToString()
            => $"Module({this.Name}, {this.Properties})";

        /// <summary>
        /// A collection of subterms.
        /// </summary>
        private struct SubtermCollection : IReadOnlyList<ITerm>
        {
            private readonly EntityATerm owner;
            
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
                        case 1: return this.owner.Properties;
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
            public SubtermCollection(EntityATerm owner)
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
