using System;
using System.Threading.Tasks;
using Brace.Commands.CommandImplementation.InternalCommands;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Subjects;
using Brace.DomainService.Command;
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
            var parameters = new[] { new CommandParameter { Name = "1" }, new CommandParameter { Name = "2" }, new CommandParameter { Name = "3" } };
            unknowCommand.SetParameters(command, subject, parameters);
            var result = await unknowCommand.ExecuteAsync();
            Assert.Equal(subject, unknowCommand.Subject);
            Assert.Equal(command, unknowCommand.CommandText);
            Assert.Equal(parameters, unknowCommand.Parameters);
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