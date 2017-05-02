using System;
using Brace.DomainModel.Cryptography;
using Brace.DomainModel.DocumentProcessing;
using Brace.Repository.Interface;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.Decrypt)]
    public class DecryptArchivist : ConfigurableArchivist
    {
        private readonly ICryptography _cryptography;
        private readonly ICryptographyRepository _cryptographyRepository;

        public DecryptArchivist(ICryptography cryptography, ICryptographyRepository cryptographyRepository)
        {
            _cryptography = cryptography;
            _cryptographyRepository = cryptographyRepository;
        }

        public override Document Rethink(Document document)
        {
            if (!string.IsNullOrEmpty(document?.Content))
            {
                if (document.IsProtected)
                {
                    var documentCryptography = _cryptographyRepository.GetByDocumentId(document.Id);
                    var decrypted = _cryptography.Decrypt(new EncryptedData
                    {
                        CipherText = Convert.FromBase64String(document.Content),
                        Iv = documentCryptography.Iv
                    }, _configuration);
                    document.Content = decrypted;
                }
            }
            if (_successor != null)
            {
                return _successor.Rethink(document);
            }
            return document;
        }
    }
}