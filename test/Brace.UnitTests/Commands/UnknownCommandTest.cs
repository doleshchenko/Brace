using System;
using System.Threading.Tasks;
using Brace.Commands.CommandImplementation.InternalCommands;
using Brace.DomainModel.DocumentProcessing;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class UnknownCommandTest
    {
        [Fact]
        public async Task ExecuteAsync_ValidParameters_ReturnsVoidResult()
        {
            var beforCommandCreated = DateTime.Now;
            var voidCommand = new UnknowCommand();
            var afterCommandCreated = DateTime.Now;
            var command = "someunknowncommand";
            var argument = "argument";
            var parameters = new[] { "1", "2", "3" };
            voidCommand.SetParameters(command, argument, parameters);
            var result = await voidCommand.ExecuteAsync();
            Assert.Equal(argument, voidCommand.Argument);
            Assert.Equal(command, voidCommand.CommandText);
            Assert.Equal(parameters, voidCommand.Parameters);
            Assert.Equal($"unknown command [{command}]", result.Content);
            Assert.Equal(DocumentViewType.Warning, result.Type);
            Assert.True(voidCommand.CreationDate >= beforCommandCreated && voidCommand.CreationDate <= afterCommandCreated);
        }
    }
}