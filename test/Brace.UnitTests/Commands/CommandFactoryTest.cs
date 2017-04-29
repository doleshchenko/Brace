using Brace.Commands;
using Brace.Commands.Factory;
using Brace.DomainService;
using Brace.DomainService.Command;
using Brace.DomainService.TypeLinker;
using Moq;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class CommandFactoryTest
    {
        [Fact]
        public void CreateCommand_ValidCommandName_CreatesCommand()
        {
            var command = "test";
            var argument = "argument";
            var parameters = new[]
            {
                new CommandParameter {Name = "1"}, new CommandParameter {Name = "2"}, new CommandParameter {Name = "3"}
            };
            var commandMock = new Mock<ICommand>();

            var commandLinkerStub = new Mock<ICommandLinker>();
            var commandProviderStub = new Mock<ISingleInterfaceServiceProvider<ICommand>>();
            commandProviderStub.Setup(it => it.Resolve(commandMock.Object.GetType())).Returns(commandMock.Object);
            commandLinkerStub.Setup(it => it.GetCommandType(command)).Returns(commandMock.Object.GetType);

            var commandFactory = new CommandFactory(commandLinkerStub.Object, commandProviderStub.Object);
            var commandInfo = new CommandInfo
            {
                Command = command,
                Subject = argument,
                Parameters = parameters
            };
            var  commandInstance = commandFactory.CreateCommand(commandInfo);
            Assert.NotNull(commandInstance);
            Assert.Equal(commandInstance, commandMock.Object);
            commandMock.Verify(it => it.SetParameters(command, argument, parameters), Times.Once);
        }

        [Fact]
        public void CreateCommand_InvalidCommandName_CreatesUnknownCommand()
        {
            var command = "someunknowncommand";
            var argument = "argument";
            var parameters = new[]
            {
                new CommandParameter {Name = "1"}, new CommandParameter {Name = "2"}, new CommandParameter {Name = "3"}
            };
            var commandMock = new Mock<ICommand>();

            var commandLinkerStub = new Mock<ICommandLinker>();
            var commandProviderStub = new Mock<ISingleInterfaceServiceProvider<ICommand>>();
            commandProviderStub.Setup(it => it.Resolve(commandMock.Object.GetType())).Returns(commandMock.Object);
            commandLinkerStub.Setup(it => it.GetCommandType(command)).Throws(new LinkerException($"Invalid command identifier - {command}. Cannot translate into CommandType."));
            commandLinkerStub.Setup(it => it.GetCommandType(CommandType.Unknown.ToString())).Returns(commandMock.Object.GetType);
            var commandFactory = new CommandFactory(commandLinkerStub.Object, commandProviderStub.Object);
            var commandInfo = new CommandInfo
            {
                Command = command,
                Subject = argument,
                Parameters = parameters
            };
            var commandInstance = commandFactory.CreateCommand(commandInfo);
            Assert.NotNull(commandInstance);
            Assert.Equal(commandInstance, commandMock.Object);
            commandMock.Verify(it => it.SetParameters(command, argument, parameters), Times.Once);
        }
    }
}