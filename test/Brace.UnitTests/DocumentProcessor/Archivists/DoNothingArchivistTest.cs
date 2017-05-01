using Brace.DocumentProcessor.Strategies.Archivists;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor.Archivists
{
    public class DoNothingArchivistTest
    {
        [Fact]
        public void Rethink_NullDocumentWithoutSuccessor_ReturnsNull()
        {
            var archivist = new DoNothingArchivist {Successor = null};
            var result = archivist.Rethink(null);
            Assert.Null(result);
        }

        [Fact]
        public void Rethink_WithoutSuccessor_ReturnsInitialObject()
        {
            var archivist = new DoNothingArchivist {Successor = null};
            var document = new Document();
            var result = archivist.Rethink(document);
            Assert.Equal(document, result);
        }

        [Fact]
        public void Rethink_WithSuccessor_CallsSuccessor()
        {
            var archivist = new DoNothingArchivist();
            var archivistMock = new Mock<IArchivist>();
            archivistMock.Setup(it => it.Successor).Returns((IArchivist) null);
            archivist.Successor = archivistMock.Object;
            var document = new Document();
            archivist.Rethink(document);
            archivistMock.Verify(it => it.Rethink(document), Times.Once);
        }
    }
}