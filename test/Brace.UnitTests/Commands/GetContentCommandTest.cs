using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.CommandImplementation.Read;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.UnitTests.Commands.Base;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class GetContentCommandTest : CommandRequiredSubjectTest<GetContentCommand>
    {
        [Fact]
        public async Task GetActionType_Void_ReturnsGetContentActionType()
        {
            await GetActionType_Void_ReturnsActionType(ActionType.GetContent);
        }

        [Fact]
        public void Validate_InvalidModifiers_ReturnsInvalidValidationResult()
        {
            Validate_InvalidModifiers_ReturnsInvalidValidationResult(CommandType.DeleteDocument, new Modifier { Name = "encrypt" });
        }

        [Theory]
        [MemberData(nameof(NullAndEmptySubject))]
        public new void Validate_NullOrEmptySubject_ReturnsInvalidValidationResult(Subject subject)
        {
            base.Validate_NullOrEmptySubject_ReturnsInvalidValidationResult(subject);
        }
    }
}