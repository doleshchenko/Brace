using System;
using System.Reflection;
using Brace.DomainService.Exceptions;

namespace Brace.DomainService
{
    public class SingleInterfaceServiceProvider<TInterface> : ISingleInterfaceServiceProvider<TInterface>
    {
        private readonly IServiceProvider _serviceProvider;

        public SingleInterfaceServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TInterface Resolve(Type concreteType)
        {
            if (!typeof(TInterface).IsAssignableFrom(concreteType))
            {
                throw new DomainServiceException($"Cannot resolve an Interface. Type {typeof(TInterface)} is not assignable from {concreteType}.");
            }
            return (TInterface) _serviceProvider.GetService(concreteType);
        }
    }
}