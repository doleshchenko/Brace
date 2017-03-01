using System.Threading.Tasks;
using Brace.DocumentProcessor.Strategies;
using Brace.DomainModel;
using Brace.Repository.Interface;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessingStrategies
{
    public class PrintDocumentStrategyTest
    {
        [Fact]
        public async Task ProcessAsync_DocumentName_ReturnsDocument()
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
            repositoryStab.Setup(repository => repository.FindDocumentAsync(expectedDocumentName)).ReturnsAsync(expectedDocument);
            var strategy = new PrintDocumentStrategy(repositoryStab.Object);
            var documentView = await strategy.ProcessAsync(expectedDocumentName, null);
            Assert.Equal(expectedContent, documentView.Content);
        }
    }
}