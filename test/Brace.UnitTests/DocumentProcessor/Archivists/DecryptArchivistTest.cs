using System;
using System.Linq;
using Brace.DocumentProcessor.Strategies.Archivists;
using Brace.DomainModel.Cryptography;
using Brace.DomainModel.DocumentProcessing;
using Brace.Repository.Interface;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor.Archivists
{
    public class DecryptArchivistTest
    {
        [Fact]
        public void Rethink_NullDocumentAndSuccessorIsConfigured_ReturnsNull()
        {
            var cryptographyStub = new Mock<ICryptography>();
            var cryptographyRepositoryStub = new Mock<ICryptographyRepository>();
            var successorMock = new Mock<IArchivist>();
            successorMock.Setup(it => it.Rethink(null)).Returns((Document)null);
            var decryptArchivist = new DecryptArchivist(cryptographyStub.Object, cryptographyRepositoryStub.Object)
                {
                    Successor = successorMock.Object
                };
            var result = decryptArchivist.Rethink(null);
            Assert.Null(result);
            successorMock.Verify(it => it.Rethink(null), Times.Once);
        }

        [Fact]
        public void Rethink_ContentIsNullAndSuccessorIsConfigured_ReturnsDocumentOfTheSuccessor()
        {
            var cryptographyStub = new Mock<ICryptography>();
            var cryptographyRepositoryStub = new Mock<ICryptographyRepository>();
            var successorMock = new Mock<IArchivist>();
            var inputDocument = new Document();
            var successorResult = new Document
            {
                Id = "1",
                Content = null
            };
            successorMock.Setup(it => it.Rethink(inputDocument)).Returns(successorResult);
            var decryptArchivist = new DecryptArchivist(cryptographyStub.Object, cryptographyRepositoryStub.Object)
                {
                    Successor = successorMock.Object
                };
            var result = decryptArchivist.Rethink(inputDocument);
            Assert.Equal(successorResult, result);
            successorMock.Verify(it => it.Rethink(inputDocument), Times.Once);
        }

        [Fact]
        public void Rethink_NullDocumentAndSuccessorIsNotConfigured_ReturnsNull()
        {
            var cryptographyStub = new Mock<ICryptography>();
            var cryptographyRepositoryStub = new Mock<ICryptographyRepository>();
            var decryptArchivist = new DecryptArchivist(cryptographyStub.Object, cryptographyRepositoryStub.Object) {Successor = null};
            var result = decryptArchivist.Rethink(null);
            Assert.Null(result);
        }

        [Fact]
        public void Rethink_DocumentContentIsNullAndSuccessorIsNotConfigured_ReturnsInputDocument()
        {
            var cryptographyStub = new Mock<ICryptography>();
            var cryptographyRepositoryStub = new Mock<ICryptographyRepository>();
            var decryptArchivist = new DecryptArchivist(cryptographyStub.Object, cryptographyRepositoryStub.Object) {Successor = null};
            var input = new Document();
            var result = decryptArchivist.Rethink(input);
            Assert.Equal(input, result);
        }

        [Fact]
        public void Rethink_UnprotectedDocumentWithContentAndSuccessorIsNotConfigured_ReturnsInputDocument()
        {
            var cryptographyStub = new Mock<ICryptography>();
            var cryptographyRepositoryStub = new Mock<ICryptographyRepository>();
            var decryptArchivist = new DecryptArchivist(cryptographyStub.Object, cryptographyRepositoryStub.Object) {Successor = null};
            var input = new Document
            {
                Content = "document content",
                IsProtected = false
            };
            var result = decryptArchivist.Rethink(input);
            Assert.Equal(input, result);
        }

        [Fact]
        public void Rethink_ProtectedDocumentWithContentAndSuccessorIsNotConfigured_ReturnsInputDocument()
        {
            var password = "password";
            var inputDocument = new Document
            {
                Content = "encrypted document content",
                IsProtected = true,
                Id = "document #1"
            };

            var documentCryptography = new DocumentCryptography
            {
                DocumentId = inputDocument.Id,
                Iv = Enumerable.Range(1, 10).Select(it => (byte) it).ToArray()
            };

            var cryptographyMock = new Mock<ICryptography>();
            var cryptographyRepositoryMock = new Mock<ICryptographyRepository>();

            cryptographyRepositoryMock.Setup(it => it.GetByDocumentId(inputDocument.Id)).Returns(documentCryptography);
            cryptographyMock.Setup(it => it.Decrypt(It.Is<EncryptedData>(encryptedData => 
                                                    encryptedData.Iv.SequenceEqual(documentCryptography.Iv) &&
                                                    encryptedData.CipherText.SequenceEqual(Convert.FromBase64String(inputDocument.Content))
                                                    )
                                                    , password))
                                                    .Returns("decrypted document content");

            var decryptArchivist = new DecryptArchivist(cryptographyMock.Object, cryptographyRepositoryMock.Object) {Successor = null};
            decryptArchivist.Configure(password);
            var result = decryptArchivist.Rethink(inputDocument);
            Assert.Equal(inputDocument, result);
            Assert.Equal(result.Content, "decrypted document content");
            cryptographyRepositoryMock.Verify(it => it.GetByDocumentId(inputDocument.Id), Times.Once);
            cryptographyMock.Verify(it => it.Decrypt(It.Is<EncryptedData>(encryptedData =>
                    encryptedData.Iv.SequenceEqual(documentCryptography.Iv) &&
                    encryptedData.CipherText.SequenceEqual(Convert.FromBase64String("encrypted document content"))), password), Times.Once);
            
        }
    }
}