using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Xunit;
using EntityLanguage.Syntax;
using Yargon.Terms;
using Yargon.Terms.ATerms;

namespace EntityLanguage.Tests
{
    public sealed class CodeGeneratorTests
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
            var ast = new AstBuilder(termFactory).Visit(tree);

            // Act
            var result = new CodeGenerator(termFactory).Generate(ast);

            // Assert
            var expectedString = @"using System;

namespace Entities
{
	public class A
	{
		public A a;
		public B b;
	}
	public class B
	{
	}
}
";
            var normalizedExpectedString = Regex.Replace(expectedString, @"\r\n|\n", Environment.NewLine);
            Assert.Equal(normalizedExpectedString, result.Value);
        }
    }
}
