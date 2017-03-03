using System;
using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;
using Brace.Repository.Interface;

namespace Brace.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        public Task<Document> FindDocumentAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Document document)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Document document)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Document document)
        {
            throw new NotImplementedException();
        }
    }
}
