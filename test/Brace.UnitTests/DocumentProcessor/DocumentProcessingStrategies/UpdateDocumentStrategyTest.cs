using System.Threading.Tasks;
using Brace.DocumentProcessor.Exceptions;
using Brace.DocumentProcessor.Strategies;
using Brace.DocumentProcessor.Strategies.Archivists;
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
    public class UpdateDocumentStrategyTest
    {
        [Fact]
        public async Task ProcessAsync_SubjectOfWrongType_ThrowsException()
        {
            var documentRepositoryStub = new Mock<IDocumentRepository>();
            var archivistFactoryStub = new Mock<IArchivistFactory>();
            var strategy = new UpdateDocumentStrategy(documentRepositoryStub.Object, archivistFactoryStub.Object);
            var subject = new DocumentIdSubject { Id = "1234567890" };
            var exception = await Assert.ThrowsAsync<DocumentProcessingStrategyException>(async () => await strategy.ProcessAsync(subject, null));
            Assert.Equal($"Invalid subject type - {typeof(DocumentIdSubject)}. It should be {typeof(UpdateDocumentSubject)}.", exception.Message);
        }

        [Fact]
        public async Task ProcessAsync_SubjectWithInvalidId_ReturnsWarningProcessingResult()
        {
            var documentRepositoryStub = new Mock<IDocumentRepository>();
            var archivistFactoryStub = new Mock<IArchivistFactory>();
            var subject = new UpdateDocumentSubject { Id = "test" };
            documentRepositoryStub.Setup(it => it.FindDocumentAsync(subject.Id)).ReturnsAsync((Document) null);
            var strategy = new UpdateDocumentStrategy(documentRepositoryStub.Object, archivistFactoryStub.Object);
            var result = await strategy.ProcessAsync(subject, null);
            Assert.NotNull(result);
            Assert.IsType<DocumentView<DocumentProcessingResultContent>>(result);
            var documentView = (DocumentView<DocumentProcessingResultContent>) result;
            Assert.Equal(DocumentViewType.Warning, documentView.Type);
            Assert.Equal(DocumentProcessingResultType.UpdateFailed, documentView.Content.ProcessingResultType);
            Assert.Equal($"Document '{subject.Id}' not found.", documentView.Content.Description);
        }

        [Fact]
        public async Task ProcessAsync_ValidSubjectWhichRequiresContentUpdate_UpdateContentAndReturnsValidProcessingResult()
        {
            var documentRepositoryMock = new Mock<IDocumentRepository>();
            var archivistFactoryMock = new Mock<IArchivistFactory>();
            var subject = new UpdateDocumentSubject {Content = "this is new content", ContentUpdateRequired = true, Id = "test_update"};
            var documentToUpdate = new Document {Content = "this is old content", Id = subject.Id};
            documentRepositoryMock.Setup(it => it.FindDocumentAsync(subject.Id)).ReturnsAsync(documentToUpdate);
            archivistFactoryMock.Setup(it => it.CreateArchivistChain(It.IsAny<DocumentProcessingAction[]>())).Returns(new DoNothingArchivist());
            var strategy = new UpdateDocumentStrategy(documentRepositoryMock.Object, archivistFactoryMock.Object);
            var result = await strategy.ProcessAsync(subject, new[] {new DocumentProcessingAction()});
            Assert.NotNull(result);
            Assert.IsType<DocumentView<DocumentProcessingResultContent>>(result);
            var documentView = (DocumentView<DocumentProcessingResultContent>) result;
            Assert.Equal(DocumentViewType.Ok, documentView.Type);
            Assert.Equal(DocumentProcessingResultType.Added, documentView.Content.ProcessingResultType);
            documentRepositoryMock.Verify(it => it.UpdateAsync(documentToUpdate),Times.Once);
            Assert.Equal("this is new content", documentToUpdate.Content);
        }

        [Fact]
        public async Task ProcessAsync_ValidSubjectWhichDoesntRequireContentUpdate_DoesNotChangeContentAndReturnsValidProcessingResult()
        {
            var documentRepositoryMock = new Mock<IDocumentRepository>();
            var archivistFactoryMock = new Mock<IArchivistFactory>();
            var subject = new UpdateDocumentSubject { Content = "this is new content", ContentUpdateRequired = false, Id = "test_update" };
            var documentToUpdate = new Document { Content = "this is old content", Id = subject.Id };
            documentRepositoryMock.Setup(it => it.FindDocumentAsync(subject.Id)).ReturnsAsync(documentToUpdate);
            archivistFactoryMock.Setup(it => it.CreateArchivistChain(It.IsAny<DocumentProcessingAction[]>())).Returns(new DoNothingArchivist());
            var strategy = new UpdateDocumentStrategy(documentRepositoryMock.Object, archivistFactoryMock.Object);
            var result = await strategy.ProcessAsync(subject, new[] { new DocumentProcessingAction() });
            Assert.NotNull(result);
            Assert.IsType<DocumentView<DocumentProcessingResultContent>>(result);
            var documentView = (DocumentView<DocumentProcessingResultContent>)result;
            Assert.Equal(DocumentViewType.Ok, documentView.Type);
            Assert.Equal(DocumentProcessingResultType.Added, documentView.Content.ProcessingResultType);
            documentRepositoryMock.Verify(it => it.UpdateAsync(documentToUpdate), Times.Once);
            Assert.Equal("this is old content", documentToUpdate.Content);
        }

        [Fact]
        public async Task ProcessAsync_ValidSubjecWithProcessingParameters_CreatesAndCallArchivistChain()
        {
            var documentRepositoryStub = new Mock<IDocumentRepository>();
            var archivistFactoryStub = new Mock<IArchivistFactory>();
            var archivistMock = new Mock<IArchivist>();
            var subject = new UpdateDocumentSubject { Id = "test" };
            archivistMock.Setup(it => it.Rethink(null)).Returns((Document)null);
            documentRepositoryStub.Setup(it => it.FindDocumentAsync(subject.Id)).ReturnsAsync(new Document());
            archivistFactoryStub.Setup(it => it.CreateArchivistChain(It.IsAny<DocumentProcessingAction[]>())).Returns(archivistMock.Object);
            var strategy = new UpdateDocumentStrategy(documentRepositoryStub.Object, archivistFactoryStub.Object);
            await strategy.ProcessAsync(subject, new[]{new DocumentProcessingAction()});
            archivistMock.Verify(it => it.Rethink(It.IsAny<Document>()), Times.Once);
        }
    }
}