using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime.Tree;
using EntityLanguage.Syntax;
using Yargon.ATerms;

namespace EntityLanguage
{
    /// <summary>
    /// Builds an AST from an ANTLR parse tree.
    /// </summary>
    public sealed class AstBuilder : EntityLanguageBaseVisitor<ITerm>
    {
        /// <summary>
        /// The term factory to use.
        /// </summary>
        private readonly ITermFactory termFactory;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AstBuilder"/> class.
        /// </summary>
        public AstBuilder(ITermFactory termFactory)
        {
            #region Contract
            if (termFactory == null)
                throw new ArgumentNullException(nameof(termFactory));
            #endregion

            this.termFactory = termFactory;
        }
        #endregion

        /// <inheritdoc />
        public override ITerm VisitStart(EntityLanguageParser.StartContext context)
        {
            #region Contract
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            #endregion

            return this.termFactory.Cons("Module",
                this.termFactory.String(context.ID().GetText()),
                this.termFactory.List(context.definition().Select(Visit)));
        }

        /// <inheritdoc />
        public override ITerm VisitDefinition(EntityLanguageParser.DefinitionContext context)
        {
            #region Contract
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            #endregion

            return this.termFactory.Cons("Entity",
                this.termFactory.String(context.ID().GetText()),
                this.termFactory.List(context.property().Select(Visit)));
        }

        /// <inheritdoc />
        public override ITerm VisitProperty(EntityLanguageParser.PropertyContext context)
        {
            #region Contract
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            #endregion

            return this.termFactory.Cons("Property",
                this.termFactory.String(context.ID().GetText()),
                Visit(context.type()));
        }

        /// <inheritdoc />
        public override ITerm VisitType(EntityLanguageParser.TypeContext context)
        {
            #region Contract
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            #endregion

            return this.termFactory.Cons("Type",
                this.termFactory.String(context.ID().GetText()));
        }
    }
}
