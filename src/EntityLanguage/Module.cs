using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLanguage
{
    public class Module
    {
        public string Name { get; set; }
        public List<Entity> Definitions { get; set; }

        #region Equality
        /// <inheritdoc />
        public bool Equals(Module other)
        {
            return !Object.ReferenceEquals(other, null)
                && this.Name == other.Name
                && this.Definitions.SequenceEqual(other.Definitions);
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
        public override bool Equals(object obj) => Equals(obj as Module);

        /// <summary>
        /// Returns a value that indicates whether two specified <see cref="Module"/> objects are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are equal;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Module left, Module right) => Object.Equals(left, right);

        /// <summary>
        /// Returns a value that indicates whether two specified <see cref="Module"/> objects are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are not equal;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Module left, Module right) => !(left == right);
        #endregion
    }
}
