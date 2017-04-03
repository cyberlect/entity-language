using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Xunit;
using EntityLanguage.Syntax;
using Yargon.Terms;
using Yargon.Terms.ATerms;

namespace EntityLanguage.Tests
{
    public sealed class AstBuilderTests
    {
        [Fact]
        public void SimpleTest()
        {
            // Arrange
            var input = new AntlrInputStream(new StringReader(@"
module X

entity A {
    a : A
    b : B
}

entity B {}"));
            var termFactory = new ATermFactory(new NullTermCache(), new Dictionary<Type, ATermFactory.TermConstructor>
            {
                [typeof(IModuleTerm)] = (f, s, a) => new ModuleATerm(f, (IStringTerm)s[0], (IListTerm<IEntityTerm>)s[1], a),
                [typeof(IEntityTerm)] = (f, s, a) => new EntityATerm(f, (IStringTerm)s[0], (IListTerm<IPropertyTerm>)s[1], a),
                [typeof(IPropertyTerm)] = (f, s, a) => new PropertyATerm(f, (IStringTerm)s[0], (ITypeTerm)s[1], a),
                [typeof(ITypeTerm)] = (f, s, a) => new TypeATerm(f, (IStringTerm)s[0], a),
            });
            var lexer = new EntityLanguageLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new EntityLanguageParser(tokens);
            IParseTree tree = parser.start();

            // Act
            var ast = new AstBuilder(termFactory).Visit(tree);

            // Assert
            var expectedAst = 
                termFactory.Create<IModuleTerm>(
                    termFactory.String("X"),
                    termFactory.List(
                        termFactory.Create<IEntityTerm>(
                            termFactory.String("A"),
                            termFactory.List(
                                termFactory.Create<IPropertyTerm>(
                                    termFactory.String("a"),
                                    termFactory.Create<ITypeTerm>(termFactory.String("A"))
                                ),
                                termFactory.Create<IPropertyTerm>(
                                    termFactory.String("b"),
                                    termFactory.Create<ITypeTerm>(termFactory.String("B"))
                                )
                            )
                        ),
                        termFactory.Create<IEntityTerm>(
                            termFactory.String("B"),
                            termFactory.List<IPropertyTerm>()
                        )
                    )
                );
            Assert.Equal(expectedAst, ast);
        }
    }
}
