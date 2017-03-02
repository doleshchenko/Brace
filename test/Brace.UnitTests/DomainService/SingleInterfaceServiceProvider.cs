using System;
using Brace.DomainService;
using Brace.DomainService.Exceptions;
using Brace.UnitTests.DomainService.Stub;
using Moq;
using Xunit;

namespace Brace.UnitTests.DomainService
{
    public class SingleInterfaceServiceProviderTest
    {
        [Fact]
        public void Resolve_ValidType_ReturnsInstance()
        {
            IForTest1 expectedResult = new ForTest1();
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sProvider => sProvider.GetService(typeof(IForTest1))).Returns(expectedResult);
            var singleInterfaceProvider = new SingleInterfaceServiceProvider<IForTest1>(serviceProviderMock.Object);
            var result = singleInterfaceProvider.Resolve(typeof(IForTest1));
            Assert.Equal(expectedResult, result);
            serviceProviderMock.Verify(it => it.GetService(typeof(IForTest1)), Times.Once);
        }

        [Fact]
        public void Resolve_InterfaceDifferentFromTheType_ThrowsException()
        {
            var serviceProviderStub = new Mock<IServiceProvider>();
            var singleInterfaceProvider = new SingleInterfaceServiceProvider<IForTest1>(serviceProviderStub.Object);
            var result = Assert.Throws<DomainServiceException>(() => singleInterfaceProvider.Resolve(typeof(ForTest2)));
            Assert.Equal($"Cannot resolve an Interface. Type {typeof(IForTest1)} is not assignable from {typeof(ForTest2)}.", result.Message);
        }
    }
}