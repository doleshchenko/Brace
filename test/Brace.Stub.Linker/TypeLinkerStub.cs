using System;
using System.Reflection;
using Brace.DomainService.TypeLinker;

namespace Brace.Stub.Linker
{
    public class TypeLinkerStub : TypeLinker<TypeLinkerAttribute,ILinkedItem, LinkerKey>
    {
        public void Initialize(Assembly assembly)
        {
            Init(assembly);
        }

        public Type GetLinkedType(LinkerKey key)
        {
            return GetLinkedTypeByKey(key);
        }
    }
}
