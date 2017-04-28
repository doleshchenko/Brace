using Brace.DocumentProcessor.Strategies.Archivists;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor.Archivists
{
    public class GetInvisibleArchivistTest
    {
        [Fact]
        public void Rethink_WithoutSuccessorAndVisibleDocument_ReturnsInitialObject()
        {
            var archivist = new GetInvisibleArchivist { Successor = null };
            var document = new Document { IsVisible = true };
            var result = archivist.Rethink(document);
            Assert.Null(result);
        }

        [Fact]
        public void Rethink_WithoutSuccessorAndInvisibleDocument_ReturnsNullObject()
        {
            var archivist = new GetInvisibleArchivist { Successor = null };
            var document = new Document { IsVisible = false };
            var result = archivist.Rethink(document);
            Assert.Equal(document, result);
        }

        [Fact]
        public void Rethink_VisibleDocumentCallToSuccessor_ReturnsNullObject()
        {
            var archivistMock = new Mock<IArchivist>();
            archivistMock.Setup(it => it.Successor).Returns((IArchivist)null);
            var archivist = new GetInvisibleArchivist { Successor = archivistMock.Object };
            var document = new Document { IsVisible = false };
            var result = archivist.Rethink(document);
            Assert.Null(result);
            archivistMock.Verify(it => it.Rethink(document), Times.Once);
        }
    }
}