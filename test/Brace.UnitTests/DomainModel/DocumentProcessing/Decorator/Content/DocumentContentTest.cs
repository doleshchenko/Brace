using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Moq;
using Xunit;

namespace Brace.UnitTests.DomainModel.DocumentProcessing.Decorator.Content
{
    public class DocumentContentTest
    {
        [Fact]
        public void ToString_ReturnsContentAsString()
        {
            var documentContentMock = new Mock<DocumentContent> {CallBase = true};
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed Need this just to call the method. I don't need the result.
            documentContentMock.Object.ToString();
            documentContentMock.Verify(it => it.ContentAsString(), Times.Once);
        }
    }
}