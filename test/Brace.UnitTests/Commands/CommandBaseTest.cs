using System;
using System.Linq;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
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
            var modifiers = new[]
            {
                new Modifier {Name = "1"}, new Modifier {Name = "2"}, new Modifier {Name = "3"}
            };
            var commandSubject = new DocumentIdSubject {Id = "subject"};

            command.SetParameters(commandText, commandSubject, modifiers);

            Assert.Equal(commandText, command.CommandText);
            Assert.Equal(commandSubject, command.Subject);
            Assert.Equal(modifiers, command.Modifiers);
        }

        [Fact]
        public async Task ExecuteAsync_ExecutesCommand()
        {
            var documentProcessorMock = new Mock<IDocumentProcessor>();
            var command = new CommandForTest(documentProcessorMock.Object);
            var commandText = "command";
            var modifiers = new[]
            {
                new Modifier {Name = "1"}, new Modifier {Name = "2"}, new Modifier {Name = "3"}
            };
            var commandSubject = new DocumentIdSubject { Id = "subject" };

            command.SetParameters(commandText, commandSubject, modifiers);
            await command.ExecuteAsync();
            var exp = modifiers.Select(parameter => new ActionParameter
                {
                    Name = parameter.Name,
                    Data = parameter.Arguments
                }).ToArray();

            documentProcessorMock.Verify(
                it => it.ProcessAsync(commandSubject, ActionType.GetContent,
                    It.Is<ActionParameter[]>(p => CompareActionParameters(p, exp))), Times.Once);
        }

        [Fact]
        public void Validate_ValidParameters_ValidValidationResult()
        {
            var documentProcessorStub = new Mock<IDocumentProcessor>();
            var command = new CommandForTest(documentProcessorStub.Object);
            var commandText = "print";
            var commandSubject = new DocumentIdSubject {Id = "test"};
            var modifiers = new[] { new Modifier { Name = "decrypt" }};

            command.SetParameters(commandText, commandSubject, modifiers);
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
            var commandSubject = new DocumentIdSubject { Id = "test" };
            var modifiers = new[] { new Modifier { Name = "decrypt" }, new Modifier { Name = "encrypt" } };

            command.SetParameters(commandText, commandSubject, modifiers);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal($"Invalid command modifiers: parameter 'encrypt' can't be used with the '{CommandType.GetContent.ToString().ToLower()}' command", validationResult.ValidationMessage);
        }

        [Fact]
        public void Validate_DuplicatedParameters_InvalidValidationResult()
        {
            var documentProcessorStub = new Mock<IDocumentProcessor>();
            var command = new CommandForTest(documentProcessorStub.Object);
            var commandText = "print";
            var commandSubject = new DocumentIdSubject { Id = "test" };
            var modifiers = new[] { new Modifier { Name = "decrypt" }, new Modifier { Name = "decrypt" } };

            command.SetParameters(commandText, commandSubject, modifiers);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal("Invalid command modifiers: duplicates found", validationResult.ValidationMessage);
        }

        private bool CompareActionParameters(ActionParameter[] collection1, ActionParameter[] collection2)
        {
            var ordered1 = collection1.OrderBy(it => it.Name).ToArray();
            var ordered2 = collection2.OrderBy(it => it.Name).ToArray();
            if (ordered1.Length != ordered2.Length)
            {
                return false;
            }
            for (var index = 0; index < ordered1.Length; index++)
            {
                if (ordered1[index].Name != ordered2[index].Name || ordered1[index].Data != ordered2[index].Data)
                {
                    return false;
                }
            }
            return true;
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