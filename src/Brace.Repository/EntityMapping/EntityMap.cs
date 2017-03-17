using Brace.DomainModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

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
                    cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                });
                _isMapped = true;
            }
        }
    }
}