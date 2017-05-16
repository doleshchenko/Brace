using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Xunit;

namespace Brace.UnitTests.DomainModel.DocumentProcessing.Decorator
{
    public class DocumentDescriptionContentTest
    {
        [Fact]
        public void ContentAsString_DocumentNameAndAttributes_StringRepresentation()
        {
            var documentName = "document 1";
            var descriptionContent = new DocumentDescriptionContent
            {
                DocumentName = documentName,
                IsDocumentProtected = true,
                IsDocumentVisible = true
            };

            var result = descriptionContent.ContentAsString();
            Assert.Equal($"name: {documentName}; visible: {true}; protected: {true}", result);
        }
    }
}