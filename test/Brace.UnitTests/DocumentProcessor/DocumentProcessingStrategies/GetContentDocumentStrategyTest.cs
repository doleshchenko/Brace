using System.Threading.Tasks;
using Brace.DocumentProcessor.Strategies;
using Brace.DocumentProcessor.Strategies.Archivists;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainModel.DocumentProcessing;
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
            archivistFactoryStub.Setup(factory => factory.CreateArchivistChain(null)).Returns(new DoNothingArhivist(null));
            var strategy = new GetContentDocumentStrategy(repositoryStab.Object, archivistFactoryStub.Object);
            var documentView = await strategy.ProcessAsync(expectedDocumentName, null);
            Assert.Equal(expectedContent, documentView.Content);
            Assert.Equal(DocumentViewType.Information, documentView.Type);
        }

        [Fact]
        public async Task ProcessAsync_DocumentNameWhichIsNotExist_ReturnsDocumentViewWithDocumentNotFoundContent()
        {
            var expectedDocumentName = "testDocument";
            var repositoryStab = new Mock<IDocumentRepository>();
            var archivistFactoryStub = new Mock<IArchivistFactory>();
            repositoryStab.Setup(repository => repository.FindDocumentAsync(expectedDocumentName)).ReturnsAsync((Document)null);
            archivistFactoryStub.Setup(factory => factory.CreateArchivistChain(null)).Returns(new DoNothingArhivist(null));
            var strategy = new GetContentDocumentStrategy(repositoryStab.Object, archivistFactoryStub.Object);
            var documentView = await strategy.ProcessAsync(expectedDocumentName, null);
            Assert.Equal($"document '{expectedDocumentName}' not found", documentView.Content);
            Assert.Equal(DocumentViewType.Warning, documentView.Type);
        }
    }
}