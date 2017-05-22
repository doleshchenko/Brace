using System;
using Brace.DomainModel.Cryptography;
using Brace.DomainModel.DocumentProcessing;
using Brace.Repository.Interface;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.Encrypt)]
    public class EncryptArchivist: ConfigurableArchivist
    {
        private readonly ICryptography _cryptography;
        private readonly ICryptographyRepository _cryptographyRepository;

        public EncryptArchivist(ICryptography cryptography, ICryptographyRepository cryptographyRepository)
        {
            _cryptography = cryptography;
            _cryptographyRepository = cryptographyRepository;
        }

        public override Document Rethink(Document document)
        {
            if (!string.IsNullOrEmpty(document?.Content))
            {
                if (!document.IsProtected)
                {
                    var encryptedData = _cryptography.Encrypt(_configuration, document.Content);
                    var cryptography = new DocumentCryptography
                    {
                        DocumentId = document.Id,
                        Iv = encryptedData.Iv
                    };
                    _cryptographyRepository.Add(cryptography);
                    document.Content = Convert.ToBase64String(encryptedData.CipherText);
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