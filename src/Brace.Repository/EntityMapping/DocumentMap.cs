using Brace.DomainModel.DocumentProcessing;
using MongoDB.Bson.Serialization;

namespace Brace.Repository.EntityMapping
{
    public class DocumentMap : EntityMap
    {
        public override void RegisterMap()
        {
            base.RegisterMap();
            BsonClassMap.RegisterClassMap<Document>(cm =>
            {
                cm.MapIdMember(c => c.Name).SetElementName("name");
                cm.MapIdMember(c => c.Content).SetElementName("content");
                cm.MapIdMember(c => c.IsProtected).SetElementName("is_protected");
            });
        }

    }
}