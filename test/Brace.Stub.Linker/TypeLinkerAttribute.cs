using System;
using Brace.DomainModel;

namespace Brace.Stub.Linker
{
    public class TypeLinkerAttribute : Attribute, ITypeLink<LinkerKey>
    {
        public LinkerKey LinkKey { get; set; }
    }
}