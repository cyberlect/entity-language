using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLanguage
{
    public class Property
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        #region Equality
        /// <inheritdoc />
        public bool Equals(Property other)
        {
            return !Object.ReferenceEquals(other, null)
                && this.Name == other.Name
                && this.Type == other.Type;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hash = 17;
            unchecked
            {
                // TODO: Implement. Don't forget the NULL checks!
                hash = hash * 29 + this.Name.GetHashCode();
                hash = hash * 29 + this.Type.GetHashCode();
            }
            return hash;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => Equals(obj as Property);

        /// <summary>
        /// Returns a value that indicates whether two specified <see cref="Property"/> objects are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are equal;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Property left, Property right) => Object.Equals(left, right);

        /// <summary>
        /// Returns a value that indicates whether two specified <see cref="Property"/> objects are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are not equal;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Property left, Property right) => !(left == right);
        #endregion
    }
}
