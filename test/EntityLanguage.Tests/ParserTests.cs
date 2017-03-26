using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using EntityLanguage.Syntax;
using Xunit;

namespace EntityLanguage.Tests
{
    public sealed class ParserTests
    {
        [Fact]
        public void Test1()
        {
            // Assert
            var input = new AntlrInputStream(new StringReader(@"
module X

entity A {
    a : A
    b : B
}

entity B {}"));
            var lexer = new EntityLanguageLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new EntityLanguageParser(tokens);

            // Act
            IParseTree tree = parser.start();

            // Assert
            string s = tree.ToStringTree(parser);
            Assert.Equal("(start module X (definition entity A { (property a : (type A)) (property b : (type B)) }) (definition entity B { }))", s);
        }
    }
}
