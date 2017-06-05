using System;
using System.Reflection;
using Brace.DocumentProcessor;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;
using Brace.Stub.TypeLinkerValidConfiguration;
using Moq;
using Moq.Protected;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor
{
    public class DocumentProcessingStrategyTypeLinkerTest
    {
        [Fact]
        public void GetArchivistType_ActionType_CallsTheBaseClassImplemetationWithTheRightParameters()
        {
            var linkerMock = new Mock<DocumentProcessingStrategyTypeLinker>(MockBehavior.Strict, typeof(TypeLinkerValidConfiguration).GetTypeInfo().Assembly) { CallBase = true };
            linkerMock.Protected().Setup<Type>("GetLinkedTypeByKey", ItExpr.IsAny<ActionType>()).Returns(typeof(IDocumentProcessingStrategy));
            linkerMock.Protected().Setup("ValidateLinksConfiguration", ItExpr.IsAny<ActionType[]>());
            var result = linkerMock.Object.GetStrategyType(ActionType.DeleteDocument);
            Assert.Equal(typeof(IDocumentProcessingStrategy), result);
        }
    }
}