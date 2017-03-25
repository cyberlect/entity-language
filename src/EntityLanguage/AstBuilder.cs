using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime.Tree;
using EntityLanguage.Syntax;

namespace EntityLanguage
{
    public class AstBuilder : EntityLanguageBaseVisitor<object>
    {
        public override object VisitStart(EntityLanguageParser.StartContext context)
        {
            return new Module()
            {
                Name = context.ID().GetText(),
                Definitions = context.definition().Select(Visit).Cast<Entity>().ToList()
            };
        }

        public override object VisitDefinition(EntityLanguageParser.DefinitionContext context)
        {
            return new Entity()
            {
                Name = context.ID().GetText(),
                Properties = context.property().Select(Visit).Cast<Property>().ToList()
            };
        }

        public override object VisitProperty(EntityLanguageParser.PropertyContext context)
        {
            return new Property()
            {
                Name = context.ID().GetText(),
                Type = (Type)Visit(context.type())
            };
        }

        public override object VisitType(EntityLanguageParser.TypeContext context)
        {
            return new Type()
            {
                Name = context.ID().GetText()
            };
        }
    }
}
