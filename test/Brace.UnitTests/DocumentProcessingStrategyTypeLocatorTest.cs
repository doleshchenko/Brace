using System.Reflection;
using Brace.DocumentProcessor;
using Brace.DomainService.DocumentProcessor;
using Brace.DomainService.TypeLinker;
using Brace.Stub.SeveralProcessingStrategiesForOneAction;
using Brace.Stub.ValidProcessingStrategies;
using Brace.Stub.WithoutProcessingStrategies;
using Xunit;

namespace Brace.UnitTests
{
    public class DocumentProcessingStrategyTypeLocatorTest
    {
        [Fact]
        public void Constructor_ValidAssembly_CreatesAnInstanceOfLocator()
        {
            new DocumentProcessingStrategyTypeLinker(typeof(ValidProcessingStrgategies).GetTypeInfo().Assembly);
        }

        [Fact]
        public void Constructor_InvalidAssemblyWitoutStrategies_ThrowsDocumentProcessorException()
        {
            var result = Assert.Throws<LinkerException>(() => new DocumentProcessingStrategyTypeLinker(typeof(WithoutProcessingStrategies).GetTypeInfo().Assembly));
            Assert.Equal("Type Links configured incorrectly. Cannot find Types for the all Keys.", result.Message);
        }

        [Fact]
        public void Constructor_InvalidAssemblySeveralStrategiesForOneActionType_ThrowsDocumentProcessorException()
        {
            var result = Assert.Throws<LinkerException>(() => new DocumentProcessingStrategyTypeLinker(typeof(SeveralProcessingStrategiesForOneAactionType).GetTypeInfo().Assembly));
            Assert.Equal($"Several types associated with the same Attribute - {ActionType.Print}. Please verify configuration.", result.Message);
        }

        [Fact]
        public void GetStrategyType_ValidAssembly_ReturnsProcessingStrategy()
        {
            var locator = new DocumentProcessingStrategyTypeLinker(typeof(ValidProcessingStrgategies).GetTypeInfo().Assembly);
            var strategyType = locator.GetStrategyType(ActionType.Print);
            Assert.Equal(typeof(PrintProcessingStrategyStub), strategyType);
        }
    }
}