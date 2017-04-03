using System;
using System.Collections.Generic;
using System.Text;
using Yargon.Terms;

namespace EntityLanguage
{
    public sealed class CodeGenerator
    {
        private ITermFactory Factory;

        public ITerm Generate(ITerm ast)
        {
            // build(pattern)
//            Factory.Module(Factory.String("X"), null);
            // match(pattern)
            // seq(a, b)
            throw new NotImplementedException();
        }
    }
}
