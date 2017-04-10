using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yargon.Terms;

namespace EntityLanguage
{
    public sealed class CodeGenerator
    {
        private readonly ITermFactory Factory;

        public CodeGenerator(ITermFactory factory)
        {
            this.Factory = factory;
        }

        public IStringTerm Generate(ITerm term)
        {
            switch (term)
            {
                case IModuleTerm m: return Generate(m);
                case IEntityTerm e: return Generate(e);
                case IPropertyTerm p: return Generate(p);
                default: return null;
            }
        }

        public IStringTerm Generate(IModuleTerm module)
        {
            return Concat(Factory, Flatten(Factory, Factory.List(
                Factory.List(Factory.String(
                    "using System;\r\n" +
                    "\r\n" +
                    "namespace Entities\r\n" +
                    "{\r\n")),
                Map(Factory, module.Definitions, Generate),
                Factory.List(Factory.String(
                    "}\r\n")))
            ));
        }

        public IStringTerm Generate(IEntityTerm entity)
        {
            return Concat(Factory, Flatten(Factory, Factory.List(
                Factory.List(Factory.String("\tpublic class "), entity.Name, Factory.String("\r\n"),
                    Factory.String("\t{\r\n")),
                Map(Factory, entity.Properties, Generate),
                Factory.List(Factory.String(
                    "\t}\r\n")))
            ));
        }

        public IStringTerm Generate(IPropertyTerm entity)
        {
            return Concat(Factory, 
                Factory.List(Factory.String("\t\tpublic "), entity.Type.Name, Factory.String(" "), entity.Name, Factory.String(";\r\n"))
            );
        }

        private IStringTerm Concat(ITermFactory factory, IListTerm<IStringTerm> strs)
        {
            var sb = new StringBuilder(strs.Sum(s => s.Value.Length));

            foreach (var s in strs)
            {
                sb.Append(s.Value);
            }

            return factory.String(sb.ToString());
        }

        private IListTerm<U> Map<T, U>(ITermFactory factory, IListTerm<T> list, Func<T, U> f)
            where T : class, ITerm
            where U : class, ITerm
        {
            return factory.List(list.Select(t => f(t)));
        }

        private IListTerm<T> Concat<T>(ITermFactory factory, IListTerm<T> list1, IListTerm<T> list2)
            where T : class, ITerm
        {
            return factory.List(list1.Concat(list2));
        }

        private IListTerm<T> Flatten<T>(ITermFactory factory, IListTerm<IListTerm<T>> list)
            where T : class, ITerm
        {
            return factory.List(list.SelectMany(t => t));
        }
    }
}
