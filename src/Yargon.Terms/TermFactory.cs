using System;
using System.Collections.Generic;
using System.Text;

namespace Yargon.Terms
{
    public class TermFactory : ITermFactory
    {
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

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IIntTerm Int(int value, IEnumerable<ITerm> annotations)
        {
            #region Contract
            if (annotations == null)
                throw new ArgumentNullException(nameof(annotations));
            #endregion

            throw new NotImplementedException();
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

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IListTerm<T> List<T>(IEnumerable<T> terms, IEnumerable<ITerm> annotations)
            where T : class, ITerm
        {
            #region Contract
            if (terms == null)
                throw new ArgumentNullException(nameof(terms));
            if (annotations == null)
                throw new ArgumentNullException(nameof(annotations));
            #endregion

            throw new NotImplementedException();
        }
    }
}
