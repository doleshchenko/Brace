using System;
using System.Collections.Generic;
using Brace.Commands;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainService.DocumentProcessor;
using Moq;
using Xunit;

namespace Brace.UnitTests.Commands.Base
{
    public abstract class CommandRequiredSubjectTest<TCommand>: CommandTestBase<TCommand> where TCommand : CommandBase
    {
        protected void Validate_NullOrEmptySubject_ReturnsInvalidValidationResult(Subject subject)
        {
            var documentProcessorStub = Mock.Of<IDocumentProcessor>();
            var command = (TCommand)Activator.CreateInstance(typeof(TCommand), documentProcessorStub);
            command.SetParameters(nameof(TCommand), subject, null);
            var validationResult = command.Validate();
            Assert.False(validationResult.IsValid);
            Assert.Equal("Invalid command subject: it's null or empty.", validationResult.ValidationMessage);
        }

        public static IEnumerable<object[]> NullAndEmptySubject => new[] {new object[] {null}, new object[] {Subject.Nothing}};
    }
}