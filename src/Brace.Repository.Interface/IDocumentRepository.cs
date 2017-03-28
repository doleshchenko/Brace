using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.Repository.Interface
{
    public interface IDocumentRepository
    {
        Task<DocumentWithoutContent[]> GetDocumentsListAsync();
        Task<Document> FindDocumentAsync(string name);
        Task AddAsync(Document document);
        Task DeleteAsync(Document document);
        Task UpdateAsync(Document document);
    }
}