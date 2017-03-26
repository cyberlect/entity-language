using System;

namespace Yargon.ATerms
{
    /// <summary>
    /// A term visitor.
    /// </summary>
    /// <typeparam name="TResult">The type of result.</typeparam>
    public interface ITermVisitor<TResult>
    {
        /// <summary>
        /// Visits a <see cref="ITerm"/>.
        /// </summary>
        /// <param name="term">The term being visited.</param>
        /// <returns>The result of visiting the term.</returns>
        TResult VisitTerm(ITerm term);

        /// <summary>
        /// Visit a <see cref="IConsTerm"/>.
        /// </summary>
        /// <param name="term">The term being visited.</param>
        /// <returns>The result of visiting the term.</returns>
        TResult VisitCons(IConsTerm term);

        /// <summary>
        /// Visit a <see cref="IIntTerm"/>.
        /// </summary>
        /// <param name="term">The term being visited.</param>
        /// <returns>The result of visiting the term.</returns>
        TResult VisitInt(IIntTerm term);

        /// <summary>
        /// Visit a <see cref="IListTerm"/>.
        /// </summary>
        /// <param name="term">The term being visited.</param>
        /// <returns>The result of visiting the term.</returns>
        TResult VisitList(IListTerm term);

        /// <summary>
        /// Visit a <see cref="IPlaceholderTerm"/>.
        /// </summary>
        /// <param name="term">The term being visited.</param>
        /// <returns>The result of visiting the term.</returns>
        TResult VisitPlaceholder(IPlaceholderTerm term);

        /// <summary>
        /// Visit a <see cref="IRealTerm"/>.
        /// </summary>
        /// <param name="term">The term being visited.</param>
        /// <returns>The result of visiting the term.</returns>
        TResult VisitReal(IRealTerm term);

        /// <summary>
        /// Visit a <see cref="IStringTerm"/>.
        /// </summary>
        /// <param name="term">The term being visited.</param>
        /// <returns>The result of visiting the term.</returns>
        TResult VisitString(IStringTerm term);
	}
}
