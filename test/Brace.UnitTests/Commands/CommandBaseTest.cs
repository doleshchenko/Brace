using System;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;
using Moq;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class CommandBaseTest
    {
        [Fact]
        public void CommandBase_ValidParameters_CreatesNewInstance()
        {
            var documentProcessorStub = new Mock<IDocumentProcessor>();
            var before = DateTime.Now;
            var command = new CommandForTest(documentProcessorStub.Object);
            var after = DateTime.Now;
            Assert.True(command.CreationDate >= before && command.CreationDate <= after);
        }

        [Fact]
        public void SetParameters_ValidParameters_SetsParameters()
        {
            var documentProcessorStub = new Mock<IDocumentProcessor>();
            var command = new CommandForTest(documentProcessorStub.Object);
            var commandText = "command";
            var commandParameters = new[] { "1", "2", "3" };
            var commandArgument = "parameter";

            command.SetParameters(commandText, commandArgument, commandParameters);

            Assert.Equal(commandText, command.CommandText);
            Assert.Equal(commandArgument, command.Argument);
            Assert.Equal(commandParameters, command.Parameters);
        }

        [Fact]
        public async Task ExecuteAsync_ExecutesCommand()
        {
            var documentProcessorMock = new Mock<IDocumentProcessor>();
            var command = new CommandForTest(documentProcessorMock.Object);
            var commandText = "command";
            var commandParameters = new[] { "1", "2", "3" };
            var commandArgument = "parameter";

            command.SetParameters(commandText, commandArgument, commandParameters);
            await command.ExecuteAsync();
            documentProcessorMock.Verify(it => it.ProcessAsync(commandArgument, ActionType.GetContent, commandParameters), Times.Once);
        }

        [Fact]
        public void Validate_ValidParameters_ValidValidationResult()
        {
            var documentProcessorStub = new Mock<IDocumentProcessor>();
            var command = new CommandForTest(documentProcessorStub.Object);
            var commandText = "print";
            var commandArgument = "test";
            var commandParameters = new[] { "decrypt" };

            command.SetParameters(commandText, commandArgument, commandParameters);
            var validationResult = command.Validate();
            Assert.True(validationResult.IsValid);
            Assert.Null(validationResult.ValidationMessage);
        }

        [Fact]
        public void Validate_InvalidParameters_InvalidValidationResult()
        {
            var documentProcessorStub = new Mock<IDocumentProcessor>();
            var command = new CommandForTest(documentProcessorStub.Object);
            var commandText = CommandType.GetContent.ToString();
            var commandArgument = "test";
            var commandParameters = new[] { "decrypt", "encrypt" };

            command.SetParameters(commandText, commandArgument, commandParameters);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal($"Invalid command parameters: parameter 'encrypt' can't be used with the '{CommandType.GetContent.ToString().ToLower()}' command", validationResult.ValidationMessage);
        }

        [Fact]
        public void Validate_DuplicatedParameters_InvalidValidationResult()
        {
            var documentProcessorStub = new Mock<IDocumentProcessor>();
            var command = new CommandForTest(documentProcessorStub.Object);
            var commandText = "print";
            var commandArgument = "test";
            var commandParameters = new[] { "decrypt", "decrypt" };

            command.SetParameters(commandText, commandArgument, commandParameters);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal("Invalid command parameters: duplicates found", validationResult.ValidationMessage);
        }
    }

    [Command(CommandType.GetContent, AssociatedArchivists = new[] { ArchivistType.Decrypt })]
    public class CommandForTest : CommandBase
    {
        public CommandForTest(IDocumentProcessor documentProcessor) : base(documentProcessor)
        {
        }

        protected override ActionType GetActionType()
        {
            return ActionType.GetContent;
        }
    }
}