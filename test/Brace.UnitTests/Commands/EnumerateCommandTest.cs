using System.Threading.Tasks;
using Brace.Commands.CommandImplementation.Read;
using Brace.DomainModel.DocumentProcessing;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class EnumerateCommandTest: CommandTestBase<EnumerateCommand>
    {
        [Fact]
        public async Task GetActionType_Void_ReturnsEnumerateActionType()
        {
            await GetActionType_Void_ReturnsActionType(ActionType.Enumerate);
        }
    }
}