using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLanguage
{
    public class Entity
    {
        public string Name { get; set; }
        public List<Property> Properties { get; set; }

        #region Equality
        /// <inheritdoc />
        public bool Equals(Entity other)
        {
            return !Object.ReferenceEquals(other, null)
                && this.Name == other.Name
                && this.Properties.SequenceEqual(other.Properties);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hash = 17;
            unchecked
            {
                // TODO: Implement. Don't forget the NULL checks!
                hash = hash * 29 + this.Name.GetHashCode();
                hash = hash * 29 + this.Properties.GetHashCode();
            }
            return hash;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => Equals(obj as Entity);

        /// <summary>
        /// Returns a value that indicates whether two specified <see cref="Entity"/> objects are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are equal;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Entity left, Entity right) => Object.Equals(left, right);

        /// <summary>
        /// Returns a value that indicates whether two specified <see cref="Entity"/> objects are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are not equal;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Entity left, Entity right) => !(left == right);
        #endregion
    }
}
