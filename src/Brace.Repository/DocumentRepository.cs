using System;
using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;
using Brace.Repository.Interface;
using MongoDB.Driver;

namespace Brace.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IMongoClient _mongoClient;

        public DocumentRepository()
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017");
        }

        public async Task<DocumentWithoutContent[]> GetDocumentsListAsync()
        {
            try
            {
                var database = _mongoClient.GetDatabase("Brace");
                var collection = database.GetCollection<DocumentWithoutContent>("documents");
                var resultList = await collection.Find(Builders<DocumentWithoutContent>.Filter.Empty).ToListAsync();
                return resultList.ToArray();
            }
            catch (Exception e)
            {
                throw new RepositoryException("Exception occured while getting the list of documents. Please see inner exception for details.", e);
            }
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
                return document;
            }
            catch (Exception e)
            {
                throw new RepositoryException($"Exception occured when searching for document '{name}'. Please see inner exception for details.", e);
            }
        }

        public async Task AddAsync(Document document)
        {
            try
            {
                var database = _mongoClient.GetDatabase("Brace");
                var collection = database.GetCollection<Document>("documents");
                await collection.InsertOneAsync(document);
            }
            catch (Exception e)
            {
                throw new RepositoryException("Exception occured when inserting a new document. Please see inner exception for details.", e);
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                var database = _mongoClient.GetDatabase("Brace");
                var collection = database.GetCollection<Document>("documents");
                var builder = Builders<Document>.Filter;
                var filter = builder.Eq(it => it.Id, id);
                await collection.DeleteOneAsync(filter);
            }
            catch (Exception e)
            {
                throw new RepositoryException($"Exception occured when deleting a document. Document id {id}. Please see inner exception for details.", e);
            }
        }

        public async Task UpdateAsync(Document document)
        {
            try
            {
                var database = _mongoClient.GetDatabase("Brace");
                var collection = database.GetCollection<Document>("documents");
                var builder = Builders<Document>.Filter;
                var filter = builder.Eq(it => it.Id, document.Id);
                await collection.ReplaceOneAsync(filter, document);
            }
            catch (Exception e)
            {
                throw new RepositoryException($"Exception occured when updating a document. Document id {document.Id}. Please see inner exception for details.", e);
            }
        }
    }
}
