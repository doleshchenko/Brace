using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Xunit;

namespace Brace.UnitTests.DomainModel.DocumentProcessing.Attributes
{
    public class DocumentProcessingStrategyAttributeTest
    {
        [Fact]
        public void Constructor_CreatesNewObject()
        {
            var expectedKey = ActionType.AddDocument;
            var attribute = new DocumentProcessingStrategyAttribute(expectedKey);
            Assert.Equal(expectedKey, attribute.LinkKey);
        }
    }
}