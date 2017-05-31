using Brace.DomainModel.DocumentProcessing;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor.Archivists
{
    public abstract class ChangeVisisbleStateArchivistTestBase<TArchivist>: ArchivistTetBase<TArchivist> where TArchivist : IArchivist, new()
    {
        protected void Rethink_WithoutSuccessorAndDocument_ReturnsNull()
        {
            var archivist = new TArchivist();
            var document = new Document();
            document.IsVisible = GetDefaultDocumentVisibleState();
            var result = archivist.Rethink(document);
            Assert.Equal(!GetDefaultDocumentVisibleState(), document.IsVisible);
            Assert.Equal(result, document);
        }

        protected void Rethink_WithSuccessorAndDocument_ReturnsNull()
        {
            var document = new Document();
            document.IsVisible = GetDefaultDocumentVisibleState();
            var successorMock = new Mock<IArchivist>();
            successorMock.Setup(it => it.Rethink(document)).Returns(document);
            var archivist = new TArchivist();
            archivist.Successor = successorMock.Object;
            var result = archivist.Rethink(document);
            Assert.Equal(!GetDefaultDocumentVisibleState(), document.IsVisible);
            Assert.Equal(result, document);
            successorMock.Verify(it => it.Rethink(document), Times.Once);
        }

        protected abstract bool GetDefaultDocumentVisibleState();
    }
}