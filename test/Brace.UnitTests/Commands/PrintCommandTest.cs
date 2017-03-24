using System.Collections.Generic;
using Brace.Commands.CommandImplementation.Read;
using Brace.DomainService.DocumentProcessor;
using Moq;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class PrintCommandTest
    {
        [Fact]
        public void Validate_InvalidParameters_InvalidValidationResult()
        {
            var documentProcessorStub = new Mock<IDocumentProcessor>();
            var command = new PrintCommand(documentProcessorStub.Object);
            var commandText = "print";
            var commandArgument = "test";
            var commandParameters = new[] { "decrypt", "encrypt" };

            command.SetParameters(commandText, commandArgument, commandParameters);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal("Invalid command parameters: parameter 'encrypt' can't be used with the 'print' command", validationResult.ValidationMessage);
        }

        [Theory]
        [MemberData(nameof(NullOrEmptyArgument))]
        public void Validate_ArgumentIsNullOrEmpty_InvalidValidationResult(string argument)
        {
            var documentProcessorStub = new Mock<IDocumentProcessor>();
            var command = new PrintCommand(documentProcessorStub.Object);
            var commandText = "print";
            var commandParameters = new[] { "decrypt" };

            command.SetParameters(commandText, argument, commandParameters);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal("Invalid command argument: document name can't be empty", validationResult.ValidationMessage);
        }

        public static IEnumerable<object[]> NullOrEmptyArgument
        {
            get
            {
                yield return new object[] {null};
                yield return new object[] {string.Empty};
                yield return new object[] {"          "};
            }
        }
    }
}