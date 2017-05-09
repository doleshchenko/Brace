using System.Collections.Generic;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.CommandImplementation.Read;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;
using Moq;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class GetContentCommandTest : CommandTestBase<GetContentCommand>
    {
        [Fact]
        public async Task GetActionType_Void_ReturnsGetContentActionType()
        {
            await GetActionType_Void_ReturnsActionType(ActionType.GetContent);
        }

        [Fact]
        public void Validate_InvalidParameters_InvalidValidationResult()
        {
            var documentProcessorStub = new Mock<IDocumentProcessor>();
            var command = new GetContentCommand(documentProcessorStub.Object);
            var commandText = CommandType.GetContent.ToString();
            var subject = new DocumentName {Id = "test"};
            var modifiers = new[] {new Modifier{Name = "decrypt"}, new Modifier { Name = "encrypt" } };

            command.SetParameters(commandText, subject, modifiers);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal($"Invalid command modifiers: parameter 'encrypt' can't be used with the '{CommandType.GetContent.ToString().ToLower()}' command", validationResult.ValidationMessage);
        }

        [Theory]
        [MemberData(nameof(NullOrEmptyArgument))]
        public void Validate_ArgumentIsNullOrEmpty_InvalidValidationResult(Subject subject)
        {
            var documentProcessorStub = new Mock<IDocumentProcessor>();
            var command = new GetContentCommand(documentProcessorStub.Object);
            var commandText = "print";
            var modifiers = new[] { new Modifier { Name = "decrypt" } };

            command.SetParameters(commandText, subject, modifiers);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal("Invalid command subject: document name can't be empty", validationResult.ValidationMessage);
        }

        public static IEnumerable<object[]> NullOrEmptyArgument
        {
            get
            {
                yield return new object[] {null};
                yield return new object[] {Subject.Nothing};
            }
        }
    }
}