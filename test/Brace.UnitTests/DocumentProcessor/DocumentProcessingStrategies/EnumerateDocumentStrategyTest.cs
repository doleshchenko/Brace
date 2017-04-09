using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brace.DocumentProcessor.Strategies;
using Brace.DocumentProcessor.Strategies.Archivists;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Brace.Repository.Interface;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor.DocumentProcessingStrategies
{
    public class EnumerateDocumentStrategyTest
    {
        [Theory]
        [MemberData(nameof(NullOrEmptyArrayOfActions))]
        public async Task ProcessAsync_NoActions_ReturnsAllDocuments(string[] actions)
        {
            var repositoryStab = new Mock<IDocumentRepository>();
            var archivistFactoryStub = new Mock<IArchivistFactory>();
            var documentInfos = new[]
            {
                new DocumentWithoutContent {Id = "1", IsProtected = true, IsVisible = true, Name = "name1"},
                new DocumentWithoutContent {Id = "12", IsProtected = false, IsVisible = true, Name = "name12"},
                new DocumentWithoutContent {Id = "123", IsProtected = true, IsVisible = false, Name = "name123"},
                
            };
            repositoryStab.Setup(it => it.GetDocumentsListAsync()).ReturnsAsync(documentInfos);
            archivistFactoryStub.Setup(it => it.CreateArchivistChain(actions)).Returns(new DoNothingArchivist());
            var strategy = new EnumerateDocumentStrategy(repositoryStab.Object, archivistFactoryStub.Object);

            var result = await strategy.ProcessAsync(string.Empty, actions);

            Assert.Equal(result.Type, DocumentViewType.Ok);
            var documentListContent = result.Content as DocumentListContent<DocumentDescriptionContent>;
            Assert.NotNull(documentListContent);
            Assert.Equal(documentInfos.Length, documentListContent.Count());
            foreach (var documentWithoutContent in documentInfos)
            {
                var content = documentListContent.FirstOrDefault(it => it.DocumentName == documentWithoutContent.Name);
                Assert.NotNull(content);
                Assert.Equal(content.IsDocumentProtected, documentWithoutContent.IsProtected);
                Assert.Equal(content.IsDocumentVisible, documentWithoutContent.IsVisible);
            }
        }

        [Fact]
        public async Task ProcessAsync_ValidAction_CallsCorrespondingArchivistToProcess()
        {
            var repositoryStab = new Mock<IDocumentRepository>();
            var archivistFactoryMock = new Mock<IArchivistFactory>();
            var visibleArchivistMock = new Mock<GetVisibleArchivist>();
            var documentInfos = new[]
            {
                new DocumentWithoutContent {Id = "1", IsProtected = true, IsVisible = true, Name = "name1"},
                new DocumentWithoutContent {Id = "12", IsProtected = false, IsVisible = true, Name = "name12"},
                new DocumentWithoutContent {Id = "123", IsProtected = true, IsVisible = false, Name = "name123"},

            };
            const string _actionName = "visible";

            repositoryStab.Setup(it => it.GetDocumentsListAsync()).ReturnsAsync(documentInfos);
            archivistFactoryMock.Setup(it => it.CreateArchivistChain(new[] {_actionName}))
                .Returns(visibleArchivistMock.Object);
            var strategy = new EnumerateDocumentStrategy(repositoryStab.Object, archivistFactoryMock.Object);

            await strategy.ProcessAsync(string.Empty, new[] {_actionName});
            archivistFactoryMock.Verify(it => it.CreateArchivistChain(new[] {_actionName}), Times.Once);
            visibleArchivistMock.Verify(it => it.Rethink(It.IsAny<Document>()), Times.Exactly(documentInfos.Length));
        }

        public static IEnumerable<object[]> NullOrEmptyArrayOfActions
        {
            get
            {
                yield return new object[] { null };
                yield return new object[] { new string[0] };
            }
        }
    }
}