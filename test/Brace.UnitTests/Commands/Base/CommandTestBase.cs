using System;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.CommandImplementation.Delete;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainService.DocumentProcessor;
using Moq;
using Xunit;

namespace Brace.UnitTests.Commands.Base
{
    public abstract class CommandTestBase<TCommand> where TCommand : CommandBase

    {
        protected async Task GetActionType_Void_ReturnsActionType(ActionType actionType)
        {
            var documentProcessorMock = new Mock<IDocumentProcessor>();
            var subject = new DocumentIdSubject {Id = "documentName"};
            var actionParameters = new ActionParameter[0];
            documentProcessorMock.Setup(it => it.ProcessAsync(subject, actionType, actionParameters))
                .ReturnsAsync(Mock.Of<DocumentView>());
            var command = (TCommand)Activator.CreateInstance(typeof(TCommand), documentProcessorMock.Object);
            command.SetParameters(string.Empty, subject, new Modifier[0]);
            await command.ExecuteAsync();
            documentProcessorMock.Verify(it => it.ProcessAsync(subject, actionType, actionParameters), Times.Once);
        }

        protected void Validate_InvalidModifiers_ReturnsInvalidValidationResult(CommandType commandType, Modifier modifier)
        {
            var documentProcessorStub = Mock.Of<IDocumentProcessor>();
            var command = new DeleteDocumentCommand(documentProcessorStub);
            var commandText = CommandType.GetContent.ToString();
            var subject = new DocumentIdSubject { Id = $"{typeof(TCommand).Name} document" };
            command.SetParameters(commandText, subject, new[] {modifier});
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal($"Invalid command modifiers: parameter '{modifier.Name}' can't be used with the '{commandType.ToString().ToLower()}' command", validationResult.ValidationMessage);
        }
    }
}