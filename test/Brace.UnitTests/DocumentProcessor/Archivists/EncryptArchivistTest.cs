using System;
using Brace.DocumentProcessor.Strategies.Archivists;
using Brace.DomainModel.Cryptography;
using Brace.DomainModel.DocumentProcessing;
using Brace.Repository.Interface;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor.Archivists
{
    public class EncryptArchivistTest: ArchivistTetBase<EncryptArchivist>
    {
        [Fact]
        public new void Rethink_WithoutSuccessorAndNull_ReturnsNull()
        {
            base.Rethink_WithoutSuccessorAndNull_ReturnsNull();
        }

        [Fact]
        public new void Rethink_WithSuccessorAndNull_ReturnsNull()
        {
            base.Rethink_WithSuccessorAndNull_ReturnsNull();
        }

        [Fact]
        public void Rethink_EncryptedDocument_ReturnsInitialDocument()
        {
            var cryptographyMock = new Mock<ICryptography>();
            var cryptographyRepositoryMock = new Mock<ICryptographyRepository>();
            var archivist = new EncryptArchivist(cryptographyMock.Object, cryptographyRepositoryMock.Object);
            var initialDocument = new Document {IsProtected = true};
            var result = archivist.Rethink(initialDocument);
            Assert.Equal(initialDocument, result);
            Assert.True(result.IsProtected);
            cryptographyMock.Verify(it => it.Encrypt(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            cryptographyRepositoryMock.Verify(it => it.Add(It.IsAny<DocumentCryptography>()), Times.Never);
        }

        [Fact]
        public void Rethink_DecryptedDocument_ReturnsEncryptedDocument()
        {
            var initialDocument = new Document { IsProtected = false, Content = "content", Id = "11111"};
            var encryptedData = new EncryptedData
            {
                CipherText = new byte[] {1, 2, 3, 4, 5, 6, 7},
                Iv = new byte[] {5, 6, 7, 7, 9}
            };
            var cryptographyMock = new Mock<ICryptography>();
            var cryptographyRepositoryMock = new Mock<ICryptographyRepository>();
            cryptographyMock.Setup(it => it.Encrypt("password", initialDocument.Content)).Returns(encryptedData);
            var archivist = new EncryptArchivist(cryptographyMock.Object, cryptographyRepositoryMock.Object);
            archivist.Configure("password");
            var result = archivist.Rethink(initialDocument);
            Assert.Equal(initialDocument, result);
            Assert.True(result.IsProtected);
            Assert.Equal(Convert.ToBase64String(new byte[] { 1, 2, 3, 4, 5, 6, 7 }), result.Content);
            cryptographyMock.Verify(it => it.Encrypt("password", "content"), Times.Once);
            cryptographyRepositoryMock.Verify(it => it.Add(It.Is<DocumentCryptography>(t => t.Iv == encryptedData.Iv && t.DocumentId == initialDocument.Id)), Times.Once);
        }
    }
}