using Brace.DomainModel.DocumentProcessing;

namespace Brace.Repository.Interface
{
    public interface ICryptographyRepository
    {
        DocumentCryptography GetByDocumentId(string documentId);
        void Add(DocumentCryptography cryptography);
    }
}