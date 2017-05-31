using System.Threading.Tasks;
using Brace.DocumentProcessor.Strategies;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Brace.Repository.Interface;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor.DocumentProcessingStrategies
{
    public class DeleteDocumentStrategyTest
    {
        [Fact]
        public async Task ProcessAsync_Subject_ReturnsValidProcessingResult()
        {
            var documentRepositoryMock = new Mock<IDocumentRepository>();
            var subject = new DocumentIdSubject {Id = "123"};
            var strategy = new DeleteDocumentStrategy(documentRepositoryMock.Object);
            var result = await strategy.ProcessAsync(subject, null);
            documentRepositoryMock.Verify(it => it.DeleteAsync(subject.Id), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<DocumentView<DocumentProcessingResultContent>>(result);
            var documentView = (DocumentView<DocumentProcessingResultContent>) result;
            Assert.Equal(DocumentViewType.Ok, documentView.Type);
            Assert.Equal(DocumentProcessingResultType.Deleted, documentView.Content.ProcessingResultType);
        }
    }
}                                                                                                                                                                                                                           