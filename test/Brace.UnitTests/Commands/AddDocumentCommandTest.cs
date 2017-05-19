using System.Collections.Generic;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.CommandImplementation.Add;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;
using Moq;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class AddDocumentCommandTest : CommandTestBase<AddDocumentCommand>
    {
        [Fact]
        public async Task GetActionType_Void_ReturnsAddDocumentActionType()
        {
            await GetActionType_Void_ReturnsActionType(ActionType.AddDocument);
        }

        [Fact]
        public void Validate_InvalidParameters_ReturnsInvalidValidationResult()
        {
            var documentProcessorStub = Mock.Of<IDocumentProcessor>();
            var command = new AddDocumentCommand(documentProcessorStub);
            var commandText = CommandType.GetContent.ToString();
            var subject = new DocumentName { Id = "new document" };
            var modifiers = new[] { new Modifier { Name = "decrypt" } };

            command.SetParameters(commandText, subject, modifiers);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal($"Invalid command modifiers: parameter 'decrypt' can't be used with the '{CommandType.AddDocument.ToString().ToLower()}' command", validationResult.ValidationMessage);
        }

        [Theory]
        [MemberData(nameof(NullAndEmptySubject))]
        public void Validate_NullOrEmptySubject_ReturnsInvalidValidationResult(Subject subject)
        {
            var documentProcessorStub = Mock.Of<IDocumentProcessor>();
            var command  = new AddDocumentCommand(documentProcessorStub);
            command.SetParameters("add", subject, null);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal("Invalid command subject: empty document.", validationResult.ValidationMessage);
        }

        public static IEnumerable<object[]> NullAndEmptySubject
        {
            get
            {
                yield return new object[] {null}; 
                yield return new object[] {Subject.Nothing};
            }
        }
    }
}