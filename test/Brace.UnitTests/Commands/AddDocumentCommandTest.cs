using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.CommandImplementation.Add;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.UnitTests.Commands.Base;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class AddDocumentCommandTest : CommandRequiredSubjectTest<AddDocumentCommand>
    {
        [Fact]
        public async Task GetActionType_Void_ReturnsAddDocumentActionType()
        {
            await GetActionType_Void_ReturnsActionType(ActionType.AddDocument);
        }

        [Fact]
        public void Validate_InvalidModifiers_ReturnsInvalidValidationResult()
        {
            Validate_InvalidModifiers_ReturnsInvalidValidationResult(CommandType.AddDocument, new Modifier { Name = "decrypt" });
        }

        [Theory]
        [MemberData(nameof(NullAndEmptySubject))]
        public new void Validate_NullOrEmptySubject_ReturnsInvalidValidationResult(Subject subject)
        {
            base.Validate_NullOrEmptySubject_ReturnsInvalidValidationResult(subject);
        }
    }
}