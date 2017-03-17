using Brace.DomainModel.DocumentProcessing;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Brace.Repository.EntityMapping
{
    public class DocumentMap : EntityMap
    {
        public override void RegisterMap()
        {
            base.RegisterMap();
            BsonClassMap.RegisterClassMap<Document>(cm =>
            {
                //cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance).SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(c => c.Name).SetElementName("name");
                cm.MapMember(c => c.Content).SetElementName("content");
                cm.MapMember(c => c.IsProtected).SetElementName("is_protected");
            });
        }

    }
}