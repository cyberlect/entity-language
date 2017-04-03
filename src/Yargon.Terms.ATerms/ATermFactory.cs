using System;
using System.Collections.Generic;
using System.Linq;

namespace Yargon.Terms.ATerms
{
    /// <summary>
    /// An ATerm factory.
    /// </summary>
    public sealed class ATermFactory : ITermFactory
    {
        /// <summary>
        /// Creates a new term.
        /// </summary>
        /// <param name="factory">The term factory.</param>
        /// <param name="subterms">The subterms of the term.</param>
        /// <param name="annotations">The annotations of the term.</param>
        /// <returns>The created term.</returns>
        public delegate ITerm TermConstructor(ITermFactory factory, IReadOnlyList<ITerm> subterms, IReadOnlyList<ITerm> annotations);

        /// <summary>
        /// The cache to use.
        /// </summary>
        private readonly ITermCache cache;

        /// <summary>
        /// A dictionary mapping term interfaces to their constructor functions.
        /// </summary>
        private readonly Dictionary<Type, TermConstructor> constructors;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ATermFactory"/> class.
        /// </summary>
        /// <param name="cache">The term cache to use.</param>
        public ATermFactory(ITermCache cache, IReadOnlyDictionary<Type, TermConstructor> constructors)
        {
            #region Contract
            if (cache == null)
                throw new ArgumentNullException(nameof(cache));
            if (constructors == null)
                throw new ArgumentNullException(nameof(constructors));
            #endregion

            this.cache = cache;
            this.constructors = constructors.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
        #endregion

        /// <inheritdoc />
        public ITerm Create(Type type, IEnumerable<ITerm> subterms, IEnumerable<ITerm> annotations)
        {
            #region Contract
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (subterms == null)
                throw new ArgumentNullException(nameof(subterms));
            if (annotations == null)
                throw new ArgumentNullException(nameof(annotations));
            #endregion

            if (!this.constructors.TryGetValue(type, out var constructor))
                throw new InvalidOperationException($"No constructor for type {type.Name} is known.");
            
            return this.cache.GetOrStore(constructor(this, subterms.ToArray(), annotations.ToArray()));
        }

        /// <inheritdoc />
        public IIntTerm Int(int value, IEnumerable<ITerm> annotations)
        {
            #region Contract
            if (annotations == null)
                throw new ArgumentNullException(nameof(annotations));
            #endregion

            return this.cache.GetOrStore(new IntTerm(this, value, annotations.ToArray()));
        }

        /// <inheritdoc />
        public IStringTerm String(string value, IEnumerable<ITerm> annotations)
        {
            #region Contract
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (annotations == null)
                throw new ArgumentNullException(nameof(annotations));
            #endregion

            return this.cache.GetOrStore(new StringTerm(this, value, annotations.ToArray()));
        }

        /// <inheritdoc />
        public IListTerm<T> List<T>(IEnumerable<T> elements, IEnumerable<ITerm> annotations)
            where T : class, ITerm
        {
            #region Contract
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));
            if (annotations == null)
                throw new ArgumentNullException(nameof(annotations));
            #endregion

            var listElements = elements.ToArray();
            var annotationElements = annotations.ToArray();

            return this.cache.GetOrStore(new ListTerm<T>(this, listElements, annotationElements));
        }
    }
}
