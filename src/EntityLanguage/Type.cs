using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLanguage
{
    public class Type
    {
        public string Name { get; set; }

        #region Equality
        /// <inheritdoc />
        public bool Equals(Type other)
        {
            return !Object.ReferenceEquals(other, null)
                && this.Name == other.Name;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hash = 17;
            unchecked
            {
                // TODO: Implement. Don't forget the NULL checks!
                hash = hash * 29 + this.Name.GetHashCode();
            }
            return hash;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => Equals(obj as Type);

        /// <summary>
        /// Returns a value that indicates whether two specified <see cref="Type"/> objects are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are equal;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Type left, Type right) => Object.Equals(left, right);

        /// <summary>
        /// Returns a value that indicates whether two specified <see cref="Type"/> objects are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are not equal;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Type left, Type right) => !(left == right);
        #endregion
    }
}
