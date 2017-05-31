using Brace.DomainModel.DocumentProcessing;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor.Archivists
{
    public abstract class ArchivistTetBase<TArchivist> where TArchivist : IArchivist, new()
    {
        protected void Rethink_WithoutSuccessorAndNull_ReturnsNull()
        {
            var archivist = new TArchivist();
            var result = archivist.Rethink(null);
            Assert.Null(result);
        }

        protected void Rethink_WithSuccessorAndNull_ReturnsNull()
        {
            var successorMock = new Mock<IArchivist>();
            successorMock.Setup(it => it.Rethink(null)).Returns((Document)null);
            var archivist = new TArchivist();
            archivist.Successor = successorMock.Object;
            var result = archivist.Rethink(null);
            Assert.Null(result);
            successorMock.Verify(it => it.Rethink(null), Times.Once);
        }
    }
}