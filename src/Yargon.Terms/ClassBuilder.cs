using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Microsoft.CSharp;

namespace Yargon.Terms
{
    public sealed class ClassBuilder
    {
        public T Build<T>(string source)
        {
//            var compiler = new CSharpCodeProvider();
//            var options = new CompilerParameters()
//            {
//                GenerateExecutable = false,
//                GenerateInMemory = true,
//            };
//            options.ReferencedAssemblies.Add("System.dll");
//            var x = compiler.CompileAssemblyFromSource(options, new string[] {source});
            throw new NotImplementedException();   
        }
    }
}
