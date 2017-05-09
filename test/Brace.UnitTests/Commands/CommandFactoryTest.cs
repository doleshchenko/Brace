using Brace.Commands;
using Brace.Commands.Factory;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainService;
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
            var subject = new DocumentName {Id = "subject"};
            var modifiers = new[]
            {
                new Modifier {Name = "1"}, new Modifier {Name = "2"}, new Modifier {Name = "3"}
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
                Subject = subject,
                Modifiers = modifiers
            };
            var  commandInstance = commandFactory.CreateCommand(commandInfo);
            Assert.NotNull(commandInstance);
            Assert.Equal(commandInstance, commandMock.Object);
            commandMock.Verify(it => it.SetParameters(command, subject, modifiers), Times.Once);
        }

        [Fact]
        public void CreateCommand_InvalidCommandName_CreatesUnknownCommand()
        {
            var command = "someunknowncommand";
            var subject = new DocumentName { Id = "subject" };
            var modifiers = new[]
            {
                new Modifier {Name = "1"}, new Modifier {Name = "2"}, new Modifier {Name = "3"}
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
                Subject = subject,
                Modifiers = modifiers
            };
            var commandInstance = commandFactory.CreateCommand(commandInfo);
            Assert.NotNull(commandInstance);
            Assert.Equal(commandInstance, commandMock.Object);
            commandMock.Verify(it => it.SetParameters(command, subject, modifiers), Times.Once);
        }
    }
}