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
            var predicates = new[]
            {
                new Predicate {Name = "1"}, new Predicate {Name = "2"}, new Predicate {Name = "3"}
            };
            var commandSubject = new DocumentName {Id = "subject"};

            command.SetParameters(commandText, commandSubject, predicates);

            Assert.Equal(commandText, command.CommandText);
            Assert.Equal(commandSubject, command.Subject);
            Assert.Equal(predicates, command.Predicates);
        }

        [Fact]
        public async Task ExecuteAsync_ExecutesCommand()
        {
            var documentProcessorMock = new Mock<IDocumentProcessor>();
            var command = new CommandForTest(documentProcessorMock.Object);
            var commandText = "command";
            var commandParameters = new[]
            {
                new Predicate {Name = "1"}, new Predicate {Name = "2"}, new Predicate {Name = "3"}
            };
            var commandSubject = new DocumentName { Id = "subject" };

            command.SetParameters(commandText, commandSubject, commandParameters);
            await command.ExecuteAsync();
            var exp = commandParameters.Select(parameter => new ActionParameter
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
            var commandSubject = new DocumentName {Id = "test"};
            var predicates = new[] { new Predicate { Name = "decrypt" }};

            command.SetParameters(commandText, commandSubject, predicates);
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
            var commandSubject = new DocumentName { Id = "test" };
            var predicates = new[] { new Predicate { Name = "decrypt" }, new Predicate { Name = "encrypt" } };

            command.SetParameters(commandText, commandSubject, predicates);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal($"Invalid command predicates: parameter 'encrypt' can't be used with the '{CommandType.GetContent.ToString().ToLower()}' command", validationResult.ValidationMessage);
        }

        [Fact]
        public void Validate_DuplicatedParameters_InvalidValidationResult()
        {
            var documentProcessorStub = new Mock<IDocumentProcessor>();
            var command = new CommandForTest(documentProcessorStub.Object);
            var commandText = "print";
            var commandSubject = new DocumentName { Id = "test" };
            var predicates = new[] { new Predicate { Name = "decrypt" }, new Predicate { Name = "decrypt" } };

            command.SetParameters(commandText, commandSubject, predicates);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal("Invalid command predicates: duplicates found", validationResult.ValidationMessage);
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