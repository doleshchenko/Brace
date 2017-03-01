using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Brace.DocumentProcessor;
using Brace.DomainService.DocumentProcessor;
using Brace.DomainService.TypeLinker;
using Brace.Stub.Linker;
using Brace.Stub.SeveralProcessingStrategiesForOneAction;
using Brace.Stub.TypeLinkerInvalidMissedLinkedItem;
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
        public void Init_InvalidAssemblyWithoutLinkedItems_ThrowsLinkerException(Assembly assembly)
        {
            var linkerStub = new TypeLinkerStub();
            var result = Assert.Throws(typeof(LinkerException), () => linkerStub.Initialize(typeof(TypeLinkerInvalidWithoutLinkedItems).GetTypeInfo().Assembly));
            Assert.Equal("Type Links configured incorrectly. Cannot find Types for the all Keys.", result.Message);
        }

        [Fact]
        public void Constructor_InvalidAssemblySeveralStrategiesForOneActionType_ThrowsDocumentProcessorException()
        {
            var result = Assert.Throws<LinkerException>(() => new DocumentProcessingStrategyTypeLinker(typeof(TypeLinkerInvalidMissedLinkedItem).GetTypeInfo().Assembly));
            Assert.Equal($"Several types associated with the same Attribute - {ActionType.Print}. Please verify configuration.", result.Message);
        }

        [Fact]
        public void GetStrategyType_ValidAssembly_ReturnsProcessingStrategy()
        {
            var linkerStub = new TypeLinkerStub();
            linkerStub.Initialize(typeof(TypeLinkerValidConfiguration).GetTypeInfo().Assembly);
            var strategyType = linkerStub.GetLinkedType(LinkerKey.Item1);
            Assert.Equal(typeof(LinkedItem1), strategyType);
        }

        public static IEnumerable<Assembly> AssembliesWithoutLinkedItems
        {
            get
            {
                yield return typeof(TypeLinkerInvalidWithoutLinkedItems).GetTypeInfo().Assembly;
                yield return typeof(TypeLinkerInvalidMissedLinkedItem).GetTypeInfo().Assembly;
            }
        }
    }
}