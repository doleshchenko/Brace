using System;

namespace Brace.DomainService
{
    public interface ISingleInterfaceServiceProvider<out TInterface>
    {
        TInterface Resolve(Type concreteType);
    }
}