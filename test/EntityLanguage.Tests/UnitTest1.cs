using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Xunit;
using EntityLanguage.Syntax;

namespace EntityLanguage.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
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
            IParseTree tree = parser.start();
            string s = tree.ToStringTree(parser);
            Assert.Equal("(start module X (definition entity A { (property a : (type A)) (property b : (type B)) }) (definition entity B { }))", s);
        }

        [Fact]
        public void Test2()
        {
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
            IParseTree tree = parser.start();

            var x = new AstBuilder().Visit(tree);
            Assert.Equal(x, new Module
            {
                Name = "X",
                Definitions = new List<Entity>
                {
                    new Entity
                    {
                        Name = "A",
                        Properties = new List<Property>
                        {
                            new Property()
                            {
                                Name = "a",
                                Type = new Type() { Name = "A" }
                            },
                            new Property()
                            {
                                Name = "b",
                                Type = new Type() { Name = "B" }
                            }
                        }
                    },
                    new Entity
                    {
                        Name = "B",
                        Properties = new List<Property>()
                    }
                }
            });
        }
    }
}
