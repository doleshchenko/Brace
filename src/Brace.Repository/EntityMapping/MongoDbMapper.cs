using System;
using System.Linq;
using System.Reflection;

namespace Brace.Repository.EntityMapping
{
    public class MongoDbMapper
    {
        public static void RegisterAllMaps()
        {
            foreach (var type in typeof(MongoDbMapper).GetTypeInfo().Assembly.GetTypes().Where(it => typeof(EntityMap).IsAssignableFrom(it) && !it.GetTypeInfo().IsAbstract))
            {
                var concreteMap = (EntityMap)Activator.CreateInstance(type);
                concreteMap.RegisterMap();
            }
        }
    }
}