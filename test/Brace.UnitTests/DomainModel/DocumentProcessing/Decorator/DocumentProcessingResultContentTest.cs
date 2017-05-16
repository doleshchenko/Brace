using System.Collections.Generic;
using System.Text.RegularExpressions;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Xunit;

namespace Brace.UnitTests.DomainModel.DocumentProcessing.Decorator
{
    public class DocumentProcessingResultContentTest
    {
        [Theory]
        [MemberData(nameof(SuccessDocumentProcessingResultTypes))]
        public void ContentAsString_SuccessResultTypeWithoutDescription_StringRepresentation(DocumentProcessingResultType resultType)
        {
            var resultContent = new DocumentProcessingResultContent {ProcessingResultType = resultType};
            var result = resultContent.ContentAsString();
            Assert.Equal($"Document {resultType}.", result);
        }

        [Theory]
        [MemberData(nameof(FailDocumentProcessingResultTypes))]
        public void ContentAsString_FailResultTypeWithoutDescription_StringRepresentation(DocumentProcessingResultType resultType)
        {
            var resultContent = new DocumentProcessingResultContent { ProcessingResultType = resultType };
            var result = resultContent.ContentAsString();
            Assert.Equal($"{Regex.Replace(resultType.ToString(), "([A-Z])", " $1").Trim()}.", result);
        }

        [Fact]
        public void ContentAsString_ResultTypeWithDescription_StringRepresentation()
        {
            var resultContent = new DocumentProcessingResultContent { ProcessingResultType = DocumentProcessingResultType.Added, Description = "It's a unit test."};
            var result = resultContent.ContentAsString();
            Assert.Equal($"Document {DocumentProcessingResultType.Added}. It's a unit test.", result);
        }

        public static IEnumerable<object[]> SuccessDocumentProcessingResultTypes
        {   
            get
            {
                yield return new object[] { DocumentProcessingResultType.Added };
                yield return new object[] { DocumentProcessingResultType.Updated };
                yield return new object[] { DocumentProcessingResultType.Deleted };
            }
        }

        public static IEnumerable<object[]> FailDocumentProcessingResultTypes
        {
            get
            {
                yield return new object[] { DocumentProcessingResultType.AddFailed };
                yield return new object[] { DocumentProcessingResultType.UpdateFailed };
                yield return new object[] { DocumentProcessingResultType.DeleteFailed };
            }
        }
    }
}