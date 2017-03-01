using System.Collections.Generic;
using System.Reflection;
using Brace.DomainService.TypeLinker;
using Brace.Stub.Linker;
using Brace.Stub.TypeLinkerInvalidMissedLinkedItem;
using Brace.Stub.TypeLinkerInvalidSeveralLinkedItemsForOneKey;
using Brace.Stub.TypeLinkerInvalidWithoutLinkedItems;
using Brace.Stub.TypeLinkerValidConfiguration;
using Xunit;
using LinkedItem1 = Brace.Stub.TypeLinkerValidConfiguration.LinkedItem1;

namespace Brace.UnitTests
{
    public class TypeLinkerTest
    {
        [Fact]
        public void Init_ValidAssembly_InitializesTheLinker()
        {
            var linkerStub = new TypeLinkerStub();
            linkerStub.Initialize(typeof(TypeLinkerValidConfiguration).GetTypeInfo().Assembly);
        }

        [Theory]
        [MemberData(nameof(AssembliesWithoutLinkedItems))]
        public void Init_InvalidAssemblyWithoutLinkedItems_ThrowsLinkerException(Assembly assembly)
        {
            var linkerStub = new TypeLinkerStub();
            var result = Assert.Throws(typeof(LinkerException), () => linkerStub.Initialize(assembly));
            Assert.Equal("Type Links configured incorrectly. Cannot find Types for the all Keys.", result.Message);
        }

        [Fact]
        public void Init_InvalidAssemblySeveralLinkedItemsForOneKey_ThrowsLinkerException()
        {
            var linkerStub = new TypeLinkerStub();
            var result = Assert.Throws(typeof(LinkerException), () => linkerStub.Initialize(typeof(TypeLinkerInvalidSeveralLinkedItemsForOneKey).GetTypeInfo().Assembly));
            Assert.Equal($"Several types associated with the same Attribute - {LinkerKey.Item2}. Please verify configuration.", result.Message);
        }

        [Fact]
        public void GetLinkedTypeByKey_ValidAssembly_ReturnsLinkedType()
        {
            var linkerStub = new TypeLinkerStub();
            linkerStub.Initialize(typeof(TypeLinkerValidConfiguration).GetTypeInfo().Assembly);
            var strategyType = linkerStub.GetLinkedType(LinkerKey.Item1);
            Assert.Equal(typeof(LinkedItem1), strategyType);
        }

        public static IEnumerable<object[]> AssembliesWithoutLinkedItems
        {
            get
            {
                yield return new object[] {typeof(TypeLinkerInvalidWithoutLinkedItems).GetTypeInfo().Assembly};
                yield return new object[] {typeof(TypeLinkerInvalidMissedLinkedItem).GetTypeInfo().Assembly};
            }
        }
    }
}