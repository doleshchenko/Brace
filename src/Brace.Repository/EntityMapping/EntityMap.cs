using Brace.DomainModel;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Brace.Repository.EntityMapping
{
    public abstract class EntityMap
    {
        private static bool _isMapped;
        public virtual void RegisterMap()
        {
            if (!_isMapped)
            {
                BsonClassMap.RegisterClassMap<Entity>(cm =>
                {
                    cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                });
                _isMapped = true;
            }
        }
    }
}