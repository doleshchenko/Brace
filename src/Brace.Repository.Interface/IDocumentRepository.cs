using System.Threading.Tasks;
using Brace.DomainModel;

namespace Brace.Repository.Interface
{
    public interface IDocumentRepository
    {
        Task<Document> FindDocumentAsync(string name);
        void Add(Document document);
        void Delete(Document document);
        void Update(Document document);
    }
}