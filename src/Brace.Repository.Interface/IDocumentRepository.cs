using System.Threading.Tasks;
using Brace.DomainModel;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.Repository.Interface
{
    public interface IDocumentRepository
    {
        Task<Document> FindDocumentAsync(string name);
        Task AddAsync(Document document);
        Task DeleteAsync(Document document);
        Task UpdateAsync(Document document);
    }
}