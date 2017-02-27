using System.Reflection;
using Brace.DocumentProcessor;
using Brace.DocumentProcessor.Exceptions;
using Brace.DomainService.DocumentProcessor;
using Brace.Stub.SeveralProcessingStrategiesForOneAction;
using Brace.Stub.ValidProcessingStrategies;
using Brace.Stub.WithoutProcessingStrategies;
using Xunit;

namespace Brace.UnitTests
{
    public class DocumentProcessingStrategyTypeLocatorTest
    {
        [Fact]
        public void Constructor_ValidAssembly_CreateAnInstanceOfLocator()
        {
            new DocumentProcessingStrategyTypeLocator(typeof(ValidProcessingStrgategies).GetTypeInfo().Assembly);
        }

        [Fact]
        public void Constructor_InvalidAssemblyWitoutStrategies_ThrowsDocumentProcessorException()
        {
            var result = Assert.Throws<DocumentProcessorException>(() => new DocumentProcessingStrategyTypeLocator(typeof(WithoutProcessingStrategies).GetTypeInfo().Assembly));
            Assert.Equal("Document processing strategies configured incorrectly. Strategies not defined for all action types.", result.Message);
        }

        [Fact]
        public void Constructor_InvalidAssemblySeveralStrategiesForOneActionType_ThrowsDocumentProcessorException()
        {
            var result = Assert.Throws<DocumentProcessorException>(() => new DocumentProcessingStrategyTypeLocator(typeof(SeveralProcessingStrategiesForOneAactionType).GetTypeInfo().Assembly));
            Assert.Equal($"Document processing strategies configured incorrectly. Several strategies associated with the same Action Type - {ActionType.Print}. Please verify configuration.", result.Message);
        }
    }
}