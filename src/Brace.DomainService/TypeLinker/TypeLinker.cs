using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Brace.DomainModel;

namespace Brace.DomainService.TypeLinker
{
    public abstract class TypeLinker<TLinkAttribute, TTargetType, TKey> 
        where TLinkAttribute : Attribute, ITypeLink<TKey>
    {
        protected Dictionary<TKey, Type> _links;

        protected void Init(Assembly assembly)
        {
            _links = new Dictionary<TKey, Type>();
            var types = assembly.GetTypes().Where(t => typeof(TTargetType).IsAssignableFrom(t)).ToArray();

            foreach (var type in types)
            {
                var classAttributes = type.GetTypeInfo().GetCustomAttributes<TLinkAttribute>().ToArray();
                if (classAttributes != null && classAttributes.Any())
                {
                    var linkAttribute = classAttributes.First();
                    var key = linkAttribute.LinkKey;
                    if (_links.ContainsKey(key))
                    {
                        throw new LinkerException($"Several types associated with the same Attribute - {key}. Please verify configuration.");
                    }
                    _links.Add(key, type);
                }
            }

            var allPossibleActions = Enum.GetValues(typeof(TKey)).Cast<TKey>().ToArray();
            if (_links.Count != allPossibleActions.Length)
            {
                throw new LinkerException("Type Links configured incorrectly. Cannot find Types for the all Keys.");
            }
        }

        protected Type GetType(TKey key)
        {
            return _links[key];
        }
    }
}