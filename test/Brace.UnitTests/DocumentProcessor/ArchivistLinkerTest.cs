using System;
using System.Reflection;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainModel.DocumentProcessing;
using Brace.Stub.TypeLinkerValidConfiguration;
using Moq;
using Moq.Protected;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor
{
    public class ArchivistLinkerTest
    {
        [Fact]
        public void GetArchivistType_ArchivistType_CallsTheBaseClassImplemetationWithTheRightParameters()
        {
            var linkerMock = new Mock<ArchivistLinker>(MockBehavior.Strict, typeof(TypeLinkerValidConfiguration).GetTypeInfo().Assembly) {CallBase = true};
            linkerMock.Protected().Setup<Type>("GetLinkedTypeByKey", ItExpr.IsAny<ArchivistType>()).Returns(typeof(IArchivist));
            linkerMock.Protected().Setup("ValidateLinksConfiguration", ItExpr.IsAny<ArchivistType[]>());
            var result = linkerMock.Object.GetArchivistType(ArchivistType.MakeVisible);
            Assert.Equal(typeof(IArchivist), result);
        }
    }
}