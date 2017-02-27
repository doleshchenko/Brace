using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Brace.DomainModel;

namespace Brace.DomainService.TypeLinker
{
    public abstract class TypeLinker<TLinkAttribute, TTargetType> 
        where TLinkAttribute : Attribute, ITypeLink<TLinkAttribute>
    {
        protected Dictionary<TLinkAttribute, Type> _links;

        protected void Init(Assembly assembly)
        {
            _links = new Dictionary<TLinkAttribute, Type>();
            var types = assembly.GetTypes().Where(t => typeof(TTargetType).IsAssignableFrom(t)).ToArray();

            foreach (var strategyType in types)
            {
                var classAttributes = strategyType.GetTypeInfo().GetCustomAttributes<TLinkAttribute>().ToArray();
                if (classAttributes != null && classAttributes.Any())
                {
                    var linkAttribute = classAttributes.First();
                    var key = linkAttribute.LinkKey;
                    if (_links.ContainsKey(key))
                    {
                        throw new LinkerException($"Several types associated with the same Attribute - {key}. Please verify configuration.");
                    }
                    _links.Add(key, strategyType);
                }
            }

            var allPossibleActions = Enum.GetValues(typeof(TLinkAttribute)).Cast<TLinkAttribute>().ToArray();
            if (_links.Count != allPossibleActions.Length)
            {
                throw new LinkerException("Type Links configured incorrectly. Cannot find Types for the all Keys.");
            }
        }
    }
}