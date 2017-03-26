using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Yargon.ATerms
{
	/// <summary>
	/// A term factory.
	/// </summary>
	/// <remarks>
	/// To implement this interface, derive from <see cref="AbstractTermFactory"/> instead.
	/// </remarks>
	public interface ITermFactory
	{
		/// <summary>
		/// Determines whether the specified term was built by this factory.
		/// </summary>
		/// <param name="term">The term to check.</param>
		/// <returns><see langword="true"/> when the term was built by this factory;
		/// otherwise, <see langword="false"/>.</returns>
		/// <remarks>
		/// <para>Any tree of terms may only consist of terms built by the same factory.</para>
		/// <para>When <paramref name="term"/> is <see langword="null"/>, this method returns
		/// <see langword="false"/>.</para>
		/// </remarks>
		[Pure]
		bool Owns(ITerm term);

	    /// <summary>
	    /// Builds a new integer term.
	    /// </summary>
	    /// <param name="value">The value of the term.</param>
	    /// <returns>The built term.</returns>
	    IIntTerm Int(int value);

		/// <summary>
		/// Builds a new integer term.
		/// </summary>
		/// <param name="value">The value of the term.</param>
		/// <param name="annotations">A set of annotations for the term.</param>
		/// <returns>The built term.</returns>
		IIntTerm Int(int value, IReadOnlyCollection<ITerm> annotations);

	    /// <summary>
	    /// Builds a new floating-point term.
	    /// </summary>
	    /// <param name="value">The value of the term.</param>
	    /// <returns>The floating-point term.</returns>
	    IRealTerm Real(float value);

		/// <summary>
		/// Builds a new floating-point term.
		/// </summary>
		/// <param name="value">The value of the term.</param>
		/// <param name="annotations">A set of annotations for the term.</param>
		/// <returns>The floating-point term.</returns>
		IRealTerm Real(float value, IReadOnlyCollection<ITerm> annotations);

	    /// <summary>
	    /// Builds a new string term.
	    /// </summary>
	    /// <param name="value">The value of the term.</param>
	    /// <returns>The string term.</returns>
	    IStringTerm String(string value);

		/// <summary>
		/// Builds a new string term.
		/// </summary>
		/// <param name="value">The value of the term.</param>
		/// <param name="annotations">A set of annotations for the term.</param>
		/// <returns>The string term.</returns>
		IStringTerm String(string value, IReadOnlyCollection<ITerm> annotations);

        /// <summary>
        /// Builds a new tuple term.
        /// </summary>
        /// <param name="terms">The sub terms of the tuple.</param>
        /// <returns>The tuple term.</returns>
        IConsTerm Tuple(params ITerm[] terms);

        /// <summary>
        /// Builds a new tuple term.
        /// </summary>
        /// <param name="terms">The sub terms of the tuple.</param>
        /// <returns>The tuple term.</returns>
        IConsTerm Tuple(IReadOnlyList<ITerm> terms);

        /// <summary>
        /// Builds a new tuple term.
        /// </summary>
        /// <param name="terms">The sub terms of the tuple.</param>
        /// <param name="annotations">A set of annotations for the term.</param>
        /// <returns>The tuple term.</returns>
        IConsTerm Tuple(IReadOnlyList<ITerm> terms, IReadOnlyCollection<ITerm> annotations);

        /// <summary>
        /// Builds a new placeholder term.
        /// </summary>
        /// <param name="template">The placeholder template.</param>
        /// <returns>The placeholder term.</returns>
        IPlaceholderTerm Placeholder(ITerm template);

        /// <summary>
        /// Builds a new placeholder term.
        /// </summary>
        /// <param name="template">The placeholder template.</param>
        /// <param name="annotations">A set of annotations for the term.</param>
        /// <returns>The placeholder term.</returns>
        IPlaceholderTerm Placeholder(ITerm template, IReadOnlyCollection<ITerm> annotations);

        /// <summary>
        /// Builds a new constructor term.
        /// </summary>
        /// <param name="name">The name of the constructor;
        /// or <see cref="System.String.Empty"/> for a tuple.</param>
        /// <param name="terms">The sub terms of the constructor.</param>
        /// <returns>The constructor term.</returns>
        IConsTerm Cons(string name, params ITerm[] terms);

        /// <summary>
        /// Builds a new constructor term.
        /// </summary>
        /// <param name="name">The name of the constructor;
        /// or <see cref="System.String.Empty"/> for a tuple.</param>
        /// <param name="terms">The sub terms of the constructor.</param>
        /// <returns>The constructor term.</returns>
        IConsTerm Cons(string name, IReadOnlyList<ITerm> terms);

        /// <summary>
        /// Builds a new constructor term.
        /// </summary>
        /// <param name="name">The name of the constructor;
        /// or <see cref="System.String.Empty"/> for a tuple.</param>
        /// <param name="terms">The sub terms of the constructor.</param>
        /// <param name="annotations">A set of annotations for the term.</param>
        /// <returns>The constructor term.</returns>
        IConsTerm Cons(string name, IReadOnlyList<ITerm> terms, IReadOnlyCollection<ITerm> annotations);

        /// <summary>
        /// Builds a new empty list term.
        /// </summary>
        /// <returns>The list term.</returns>
        IListTerm EmptyList();

        /// <summary>
        /// Builds a new empty list term.
        /// </summary>
        /// <param name="annotations">A set of annotations for the term.</param>
        /// <returns>The list term.</returns>
        IListTerm EmptyList(IReadOnlyCollection<ITerm> annotations);

        /// <summary>
        /// Builds a new list term.
        /// </summary>
        /// <param name="head">The head of the list.</param>
        /// <param name="tail">The tail of the list.</param>
        /// <returns>The list term.</returns>
        IListTerm ListConsNil(ITerm head, IListTerm tail);

		/// <summary>
		/// Builds a new list term.
		/// </summary>
		/// <param name="head">The head of the list.</param>
		/// <param name="tail">The tail of the list.</param>
		/// <param name="annotations">A set of annotations for the term.</param>
		/// <returns>The list term.</returns>
		IListTerm ListConsNil(ITerm head, IListTerm tail, IReadOnlyCollection<ITerm> annotations);

	    /// <summary>
	    /// Builds a new list term.
	    /// </summary>
	    /// <param name="terms">The terms in the list.</param>
	    /// <returns>The list term.</returns>
	    IListTerm List(IEnumerable<ITerm> terms);

	    /// <summary>
	    /// Builds a new list term.
	    /// </summary>
	    /// <param name="terms">The terms in the list.</param>
	    /// <param name="annotations">A set of annotations for the term.</param>
	    /// <returns>The list term.</returns>
	    IListTerm List(IEnumerable<ITerm> terms, IReadOnlyCollection<ITerm> annotations);

	    /// <summary>
	    /// Builds a new list term.
	    /// </summary>
	    /// <param name="terms">The terms in the list.</param>
	    /// <returns>The list term.</returns>
	    IListTerm List(params ITerm[] terms);
	}
}
