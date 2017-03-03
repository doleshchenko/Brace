using System.Reflection;
using Brace.Commands.Factory;
using Brace.DomainService.TypeLinker;
using Brace.Stub.CommandLinker;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class CommandLinkerTest
    {
        private readonly CommandLinker _commandLinker;

        public CommandLinkerTest()
        {
            _commandLinker = new CommandLinker(typeof(CommandStubMain).GetTypeInfo().Assembly);
        }

        [Fact]
        public void GetCommandType_ValidCommand_ReturnsCommand()
        {
            var command = "print";
            var commandType = _commandLinker.GetCommandType(command);
            Assert.Equal(typeof(PrintCommand), commandType);
        }

        [Fact]
        public void GetCommandType_InvalidCommand_ReturnsCommand()
        {
            var command = "invalidcommand";
            var result = Assert.Throws<LinkerException>(() => _commandLinker.GetCommandType(command));
            Assert.Equal($"Invalid command identifier - {command}. Cannot translate into CommandType.", result.Message);
        }
    }
}