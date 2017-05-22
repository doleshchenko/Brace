using System.Threading.Tasks;
using Brace.DocumentProcessor.Exceptions;
using Brace.DocumentProcessor.Strategies;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Brace.Repository.Interface;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor.DocumentProcessingStrategies
{
    public class AddDocumentStrategyTest
    {
        [Fact]
        public async Task ProcessAsync_SubjectOfWrongType_ThrowsException()
        {
            var documentRepositoryStub = new Mock<IDocumentRepository>();
            var archivistFactoryStub = new Mock<IArchivistFactory>();
            var strategy = new AddDocumentStrategy(documentRepositoryStub.Object, archivistFactoryStub.Object);
            var subject = new DocumentIdSubject {Id = "1234567890"};
            var exception = await Assert.ThrowsAsync<DocumentProcessingStrategyException>(async () => await strategy.ProcessAsync(subject, null));
            Assert.Equal($"Invalid subject type - {typeof(DocumentIdSubject)}. It should be {typeof(AddDocumentSubject)}.", exception.Message);
        }

        [Fact]
        public async Task ProcessAsync_ValidSubject_ReturnsOkResult()
        {
            var documentContent = "document content";
            var documentId = "12345";
            var documentRepositoryMock = new Mock<IDocumentRepository>();
            var archivistFactoryMock = new Mock<IArchivistFactory>();
            var archivistMock = new Mock<IArchivist>();
            documentRepositoryMock.Setup(it => it.AddAsync(It.Is<Document>(d => d.Id == null && d.Content == documentContent))).Returns(Task.CompletedTask).Callback<Document>(it => it.Id = documentId);
            archivistFactoryMock.Setup(it => it.CreateArchivistChain(null)).Returns(archivistMock.Object);
            archivistMock.Setup(it => it.Rethink(It.Is<Document>(d => d.Id == documentId))).Returns<Document>(d => d);
            var strategy = new AddDocumentStrategy(documentRepositoryMock.Object, archivistFactoryMock.Object);
            var subject = new AddDocumentSubject{Content = documentContent};
            var result = await strategy.ProcessAsync(subject, null);
            documentRepositoryMock.Verify(it => it.AddAsync(It.Is<Document>(d => d.Id == documentId)), Times.Once);
            documentRepositoryMock.Verify(it => it.UpdateAsync(It.Is<Document>(d => d.Id == documentId)),Times.Once);
            archivistFactoryMock.Verify(it => it.CreateArchivistChain(null), Times.Once);
            archivistMock.Verify(it => it.Rethink(It.Is<Document>(d=> d.Id == documentId)), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<DocumentView<DocumentProcessingResultContent>>(result);
            Assert.Equal(DocumentViewType.Ok, result.Type);
            var resultContent = (DocumentProcessingResultContent)result.Content;
            Assert.Equal(DocumentProcessingResultType.Added, resultContent.ProcessingResultType);
            Assert.Null(resultContent.Description);
        }
    }
}