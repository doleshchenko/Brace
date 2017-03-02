using System;

namespace Brace.DomainService.DocumentProcessor
{
    public interface ISingleInterfaceServiceProvider<TInterface>
    {
        TInterface Resolve(Type concreteType);
    }
}