using System.Linq;
using System.Threading.Tasks;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Brace.DomainService;
using Brace.DomainService.DocumentProcessor;
using Moq;
using Xunit;

namespace Brace.UnitTests.DocumentProcessor
{
    public class DocumentProcessorTest
    {
        [Fact]
        public async Task ProcessAsync_SubjectActionTypeAndActionParameter_CallsProcessingStrategyWithValidParameters()
        {
            var subject = new DocumentIdSubject();
            var processingActions = new []
            {
                new ActionParameter {Name = "action1", Data = "some data"},
                new ActionParameter {Name = "action2"}
            };
            var result = new DocumentView<DocumentProcessingResultContent>();
            var linkerMock = new Mock<IDocumentProcessingStrategyTypeLinker>();
            var interfaceProviderMock = new Mock<ISingleInterfaceServiceProvider<IDocumentProcessingStrategy>>();
            var strategyMock = new Mock<IDocumentProcessingStrategy>();
            var action = ActionType.AddDocument;
            var strategyType = typeof(IDocumentProcessingStrategy);
            linkerMock.Setup(it => it.GetStrategyType(action)).Returns(strategyType);
            interfaceProviderMock.Setup(it => it.Resolve(strategyType)).Returns(strategyMock.Object);
            strategyMock.Setup(it => it.ProcessAsync(subject, It.IsAny<DocumentProcessingAction[]>())).ReturnsAsync(result);

            var processor = new Brace.DocumentProcessor.DocumentProcessor(linkerMock.Object, interfaceProviderMock.Object);
            var documentView = await processor.ProcessAsync(subject, ActionType.AddDocument, processingActions);
            Assert.Equal(result, documentView);
            strategyMock.Verify(it => it.ProcessAsync(subject, It.Is<DocumentProcessingAction[]>(x => x != null && VerifyThatAllParametersWereConvertedCorrectly(x, processingActions))), Times.Once);
        }

        private bool VerifyThatAllParametersWereConvertedCorrectly(DocumentProcessingAction[] result, ActionParameter[] source)
        {
            foreach (var actionParameter in source)
            {
                if (!result.Any(it => it.ActionName == actionParameter.Name && it.RequiredData == actionParameter.Data))
                {
                    return false;
                }
            }
            return true;
        }
    }
}