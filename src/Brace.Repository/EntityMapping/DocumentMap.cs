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
                cm.MapMember(c => c.Name).SetElementName("name");
                cm.MapMember(c => c.Content).SetElementName("content");
                cm.MapMember(c => c.IsVisible).SetElementName("is_visible");
                cm.MapMember(c => c.IsProtected).SetElementName("is_protected");
            });
        }
    }
}