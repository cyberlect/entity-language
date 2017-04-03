using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Yargon.Terms;
using Yargon.Terms.ATerms;

namespace EntityLanguage
{
    /// <summary>
    /// A module.
    /// </summary>
    public sealed class ModuleATerm : Term, IModuleTerm
    {
        private readonly SubtermCollection subterms;
        /// <inheritdoc />
        public override IReadOnlyList<ITerm> Subterms => this.subterms;

        /// <inheritdoc />
        public IStringTerm Name { get; }

        /// <inheritdoc />
        public IListTerm<IEntityTerm> Definitions { get; }

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleATerm"/> class.
        /// </summary>
        /// <param name="factory">The factory that created this term.</param>
        /// <param name="name">The name.</param>
        /// <param name="definitions">The definitions.</param>
        /// <param name="annotations">The annotations of the term.</param>
        public ModuleATerm(ITermFactory factory, IStringTerm name, IListTerm<IEntityTerm> definitions, IReadOnlyCollection<ITerm> annotations)
            : base(factory, annotations)
        {
            #region Contract
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (definitions == null)
                throw new ArgumentNullException(nameof(definitions));
            #endregion

            this.Name = name;
            this.Definitions = definitions;
            this.subterms = new SubtermCollection(this);
        }
        #endregion

        #region Equality
        /// <inheritdoc />
        public bool Equals(ModuleATerm other)
        {
            if (Object.ReferenceEquals(this, other))
                return true;

            return base.Equals(other)
                && this.Name.Equals(other.Name)
                && this.Definitions.Equals(other.Definitions);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hash = 17;
            unchecked
            {
                hash = hash * 29 + this.Name.GetHashCode();
                hash = hash * 29 + this.Definitions.GetHashCode();
            }
            return hash;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => Equals(obj as ModuleATerm);
        #endregion

        /// <inheritdoc />
        public override string ToString()
            => $"Module({this.Name}, {this.Definitions})";

        /// <summary>
        /// A collection of subterms.
        /// </summary>
        private struct SubtermCollection : IReadOnlyList<ITerm>
        {
            private readonly ModuleATerm owner;
            
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
                        case 1: return this.owner.Definitions;
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
            public SubtermCollection(ModuleATerm owner)
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
