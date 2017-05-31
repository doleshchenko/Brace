using Brace.DocumentProcessor.Strategies.Archivists;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor.Archivists
{
    public class MakeInvisibleArchivistTest : ChangeVisisbleStateArchivistTestBase<MakeInvisibleArchivist>
    {
        [Fact]
        public new void Rethink_WithoutSuccessorAndNull_ReturnsNull()
        {
            base.Rethink_WithoutSuccessorAndNull_ReturnsNull();
        }

        [Fact]
        public new void Rethink_WithSuccessorAndNull_ReturnsNull()
        {
            base.Rethink_WithSuccessorAndNull_ReturnsNull();
        }

        [Fact]
        public void Rethink_WithoutSuccessorAndVisibleDocument_ReturnsNull()
        {
            Rethink_WithoutSuccessorAndDocument_ReturnsNull();
        }

        [Fact]
        public void Rethink_WithSuccessorAndVisibleDocument_ReturnsNull()
        {
            Rethink_WithSuccessorAndDocument_ReturnsNull();
        }

        protected override bool GetDefaultDocumentVisibleState()
        {
            return true;
        }
    }
}