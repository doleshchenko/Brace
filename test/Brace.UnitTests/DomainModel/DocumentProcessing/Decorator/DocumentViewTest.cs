using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Moq;
using Xunit;

namespace Brace.UnitTests.DomainModel.DocumentProcessing.Decorator
{
    public class DocumentViewTest
    {
        [Fact]
        public void ToString_NullContent_ReturnsNull()
        {
            var documentView = new DocumentView<DocumentContent> {Content = null};
            var result = documentView.ToString();
            Assert.Null(result);
        }

        [Fact]
        public void ToString_NotNullContent_ReturnsNull()
        {
            var documentContentStub = new Mock<DocumentContent>();
            documentContentStub.Setup(it => it.ContentAsString()).Returns("test");
            var documentView = new DocumentView<DocumentContent> { Content = documentContentStub.Object };
            var result = documentView.ToString();
            Assert.Equal("test", result);
        }
    }
}