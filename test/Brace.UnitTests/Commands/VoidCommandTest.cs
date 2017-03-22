using System.Threading.Tasks;
using Brace.Commands;
using Brace.DomainModel.DocumentProcessing;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class VoidCommandTest
    {
        [Fact]
        public async Task ExecuteAsync_ValidParameters_ReturnsVoidResult()
        {
            var voidCommand = new VoidCommand();
            voidCommand.SetParameters(null, null);
            var result = await voidCommand.ExecuteAsync();
            Assert.Equal("void command", result.Content);
            Assert.Equal(DocumentViewType.Warning, result.Type);
        }
    }
}