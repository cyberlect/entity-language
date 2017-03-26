using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Xunit;
using EntityLanguage.Syntax;
using Yargon.ATerms;

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
            var termFactory = new TrivialTermFactory();
            var lexer = new EntityLanguageLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new EntityLanguageParser(tokens);
            IParseTree tree = parser.start();

            // Act
            var ast = new AstBuilder(termFactory).Visit(tree);

            // Assert
            var expectedAst = 
                termFactory.Cons("Module",
                    termFactory.String("X"),
                    termFactory.List(
                        termFactory.Cons("Entity",
                            termFactory.String("A"),
                            termFactory.List(
                                termFactory.Cons("Property",
                                    termFactory.String("a"),
                                    termFactory.Cons("Type", termFactory.String("A"))
                                ),
                                termFactory.Cons("Property",
                                    termFactory.String("b"),
                                    termFactory.Cons("Type", termFactory.String("B"))
                                )
                            )
                        ),
                        termFactory.Cons("Entity",
                            termFactory.String("B"),
                            termFactory.List()
                        )
                    )
                );
            Assert.Equal(expectedAst, ast);
        }
    }
}
