using System;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainService.DocumentProcessor;
using Moq;

namespace Brace.UnitTests.Commands
{
    public class CommandTestBase<TCommand> where TCommand : CommandBase

    {
        protected async Task GetActionType_Void_ReturnsActionType(ActionType actionType)
        {
            var documentProcessorMock = new Mock<IDocumentProcessor>();
            const string documentName = "documentName";
            documentProcessorMock.Setup(it => it.ProcessAsync(documentName, actionType, new string[0]))
                .ReturnsAsync(Mock.Of<DocumentView>());
            var command = (TCommand)Activator.CreateInstance(typeof(TCommand), documentProcessorMock.Object);
            command.SetParameters(string.Empty, documentName, new string[0]);
            await command.ExecuteAsync();
            documentProcessorMock.Verify(it => it.ProcessAsync(documentName, actionType, new string[0]), Times.Once);
        }
    }
}