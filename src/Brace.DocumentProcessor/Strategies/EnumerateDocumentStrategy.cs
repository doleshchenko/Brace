using System.Collections.Generic;
using System.Threading.Tasks;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainService.DocumentProcessor;
using Brace.Repository.Interface;

namespace Brace.DocumentProcessor.Strategies
{
    [DocumentProcessingStrategy(ActionType.Enumerate)]
    public class EnumerateDocumentStrategy : IDocumentProcessingStrategy
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IArchivistFactory _archivistFactory;

        public EnumerateDocumentStrategy(IDocumentRepository documentRepository, IArchivistFactory archivistFactory)
        {
            _documentRepository = documentRepository;
            _archivistFactory = archivistFactory;
        }

        public async Task<DocumentView> ProcessAsync(string documentName, string[] actions)
        {
            var allDocumnents = await _documentRepository.FindDocumentsAsync();
            var archivist = _archivistFactory.CreateArchivistChain(actions);
            var resultedDocuments = new List<Document>();
            foreach (var document in allDocumnents)
            {
                var result = archivist.Rethink(document);
                if (result != null)
                {
                    resultedDocuments.Add(result);
                }
            }
            throw new System.NotImplementedException();
        }
    }
}