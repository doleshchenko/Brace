using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.CommandImplementation.Update;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.UnitTests.Commands.Base;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class UpdateDocumentCommandTest : CommandRequiredSubjectTest<UpdateDocumentCommand>
    {
        [Fact]
        public async Task GetActionType_Void_ReturnsUpdateDocumentActionType()
        {
            await GetActionType_Void_ReturnsActionType(ActionType.UpdateDocument);
        }

        [Fact]
        public void Validate_InvalidModifiers_ReturnsInvalidValidationResult()
        {
            Validate_InvalidModifiers_ReturnsInvalidValidationResult(CommandType.UpdateDocument, new Modifier { Name = "invisible" });
        }

        [Theory]
        [MemberData(nameof(NullAndEmptySubject))]
        public new void Validate_NullOrEmptySubject_ReturnsInvalidValidationResult(Subject subject)
        {
            base.Validate_NullOrEmptySubject_ReturnsInvalidValidationResult(subject);
        }
    }
}