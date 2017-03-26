using System;

namespace Yargon.ATerms
{
	/// <summary>
	/// A term visitor.
	/// </summary>
	public interface ITermVisitor
	{
	    /// <summary>
	    /// Visits a <see cref="ITerm"/>.
	    /// </summary>
	    /// <param name="term">The term being visited.</param>
	    void VisitTerm(ITerm term);

	    /// <summary>
        /// Visit a <see cref="IConsTerm"/>.
        /// </summary>
        /// <param name="term">The term being visited.</param>
        void VisitCons(IConsTerm term);

		/// <summary>
		/// Visit a <see cref="IIntTerm"/>.
		/// </summary>
		/// <param name="term">The term being visited.</param>
		void VisitInt(IIntTerm term);

		/// <summary>
		/// Visit a <see cref="IListTerm"/>.
		/// </summary>
		/// <param name="term">The term being visited.</param>
		void VisitList(IListTerm term);

		/// <summary>
		/// Visit a <see cref="IPlaceholderTerm"/>.
		/// </summary>
		/// <param name="term">The term being visited.</param>
		void VisitPlaceholder(IPlaceholderTerm term);

		/// <summary>
		/// Visit a <see cref="IRealTerm"/>.
		/// </summary>
		/// <param name="term">The term being visited.</param>
		void VisitReal(IRealTerm term);

		/// <summary>
		/// Visit a <see cref="IStringTerm"/>.
		/// </summary>
		/// <param name="term">The term being visited.</param>
		void VisitString(IStringTerm term);
	}
}
