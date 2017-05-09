using System.Threading.Tasks;
using Brace.DocumentProcessor.Strategies;
using Brace.DocumentProcessor.Strategies.Archivists;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.Repository.Interface;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor.DocumentProcessingStrategies
{
    public class GetContentDocumentStrategyTest
    {
        [Fact]
        public async Task ProcessAsync_DocumentName_ReturnsDocumentViewWithDocumentContent()
        {
            var expectedDocumentName = "testDocument";
            var expectedContent = "document content";
            var expectedDocument = new Document
            {
                Name = expectedDocumentName,
                Content = expectedContent,
                IsProtected = false
            };
            var repositoryStab = new Mock<IDocumentRepository>();
            var archivistFactoryStub = new Mock<IArchivistFactory>();
            repositoryStab.Setup(repository => repository.FindDocumentAsync(expectedDocumentName)).ReturnsAsync(expectedDocument);
            archivistFactoryStub.Setup(factory => factory.CreateArchivistChain(null)).Returns(new DoNothingArchivist());
            var strategy = new GetContentDocumentStrategy(repositoryStab.Object, archivistFactoryStub.Object);
            var documentView = await strategy.ProcessAsync(new DocumentName{Id = expectedDocumentName}, null);
            Assert.Equal(expectedContent, documentView.Content.ContentAsString());
            Assert.Equal(DocumentViewType.Ok, documentView.Type);
        }

        [Fact]
        public async Task ProcessAsync_DocumentNameWhichIsNotExist_ReturnsDocumentViewWithDocumentNotFoundContent()
        {
            var expectedDocumentName = "testDocument";
            var repositoryStab = new Mock<IDocumentRepository>();
            var archivistFactoryStub = new Mock<IArchivistFactory>();
            repositoryStab.Setup(repository => repository.FindDocumentAsync(expectedDocumentName)).ReturnsAsync((Document)null);
            archivistFactoryStub.Setup(factory => factory.CreateArchivistChain(null)).Returns(new DoNothingArchivist());
            var strategy = new GetContentDocumentStrategy(repositoryStab.Object, archivistFactoryStub.Object);
            var documentView = await strategy.ProcessAsync(new DocumentName { Id = expectedDocumentName }, null);
            Assert.Equal($"document '{expectedDocumentName}' not found", documentView.Content.ContentAsString());
            Assert.Equal(DocumentViewType.Warning, documentView.Type);
        }
    }
}