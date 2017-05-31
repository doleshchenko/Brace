using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Xunit;

namespace Brace.UnitTests.DomainModel.DocumentProcessing.Attributes
{
    public class ArchivistAttributeTest
    {
        [Fact]
        public void Constructor_CreatesNewObject()
        {
            var expectedKey = ArchivistType.Encrypt;
            var attribute = new ArchivistAttribute(expectedKey);
            Assert.Equal(expectedKey, attribute.LinkKey);
        }
    }
}