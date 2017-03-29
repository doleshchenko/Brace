using System;
using System.Threading.Tasks;
using Brace.Commands.CommandImplementation.InternalCommands;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class VoidCommandTest
    {
        [Fact]
        public async Task ExecuteAsync_ValidParameters_ReturnsVoidResult()
        {
            var beforCommandCreated = DateTime.Now;
            var voidCommand = new VoidCommand();
            var afterCommandCreated = DateTime.Now;
            voidCommand.SetParameters(null, null, null);
            var result = await voidCommand.ExecuteAsync();
            Assert.Equal("void command", result.Content.ContentAsString());
            Assert.Equal(DocumentViewType.Warning, result.Type);
            Assert.True(voidCommand.CreationDate >= beforCommandCreated && voidCommand.CreationDate <= afterCommandCreated);
        }
    }
}