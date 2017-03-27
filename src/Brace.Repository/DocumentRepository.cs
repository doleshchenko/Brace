using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;
using Brace.Repository.Interface;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Brace.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IMongoClient _mongoClient;

        public DocumentRepository()
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017");
        }

        public async Task<Document> FindDocumentAsync(string name)
        {
            try
            {
                var builder = Builders<Document>.Filter;
                var filter = builder.Eq(it => it.Name, name);
                var database = _mongoClient.GetDatabase("Brace");
                var collection = database.GetCollection<Document>("documents");
                var document = await collection.Find(filter).FirstOrDefaultAsync();
                //Expression<Func<Document, bool>> myMethod = null;
                return document;
            }
            catch (Exception e)
            {
                throw new RepositoryException($"Exception occured when searching for document '{name}'. Please see inner exception for details.", e);
            }
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
