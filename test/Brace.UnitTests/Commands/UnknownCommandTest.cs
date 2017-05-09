using System;
using System.Threading.Tasks;
using Brace.Commands.CommandImplementation.InternalCommands;
using Brace.Commands.Validation;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Xunit;

namespace Brace.UnitTests.Commands
{
    public class UnknownCommandTest
    {
        [Fact]
        public async Task ExecuteAsync_ValidParameters_ReturnsVoidResult()
        {
            var beforCommandCreated = DateTime.Now;
            var unknowCommand = new UnknowCommand();
            var afterCommandCreated = DateTime.Now;
            var command = "someunknowncommand";
            var subject = new DocumentName {Id = "subject"};
            var modifiers = new[] { new Modifier { Name = "1" }, new Modifier { Name = "2" }, new Modifier { Name = "3" } };
            unknowCommand.SetParameters(command, subject, modifiers);
            var result = await unknowCommand.ExecuteAsync();
            Assert.Equal(subject, unknowCommand.Subject);
            Assert.Equal(command, unknowCommand.CommandText);
            Assert.Equal(modifiers, unknowCommand.Modifiers);
            Assert.Equal($"unknown command [{command}]", result.Content.ContentAsString());
            Assert.Equal(DocumentViewType.Warning, result.Type);
            Assert.True(unknowCommand.CreationDate >= beforCommandCreated && unknowCommand.CreationDate <= afterCommandCreated);
        }

        [Fact]
        public void Validate_Void_ReurnsValidResult()
        {
            var unknowCommand = new UnknowCommand();
            var validationResult = unknowCommand.Validate();
            Assert.Equal(CommandValidationResult.Valid, validationResult);
        }
    }
}