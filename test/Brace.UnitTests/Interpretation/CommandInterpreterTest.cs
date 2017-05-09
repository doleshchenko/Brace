using System.Linq;
using Brace.DomainModel.Command.Subjects;
using Brace.Interpretation;
using Brace.Interpretation.Exceptions;
using Xunit;

namespace Brace.UnitTests.Interpretation
{
    public class CommandInterpreterTest
    {
        private readonly CommandInterpreter _commandInterpreter;
        public CommandInterpreterTest()
        {
            _commandInterpreter = new CommandInterpreter();
        }

        [Fact]
        public void Iterpret_EmptyCommand_ReturnsEmptyInterpretation()
        {
            var sentence = string.Empty;
            var interpretation = _commandInterpreter.Interpret(sentence);
            Assert.Equal(CommandInterpretation.EmptyInterpretation, interpretation);
        }

        [Fact]
        public void Interpret_CommandWithSubject_ReturnsValidInterpretation()
        {
            var sentence = "getcontent {mystery}";
            var interpretation = _commandInterpreter.Interpret(sentence);
            var expectedCommand = "getcontent";
            var expectedArgument = "mystery";
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Equal(expectedArgument, interpretation.Subject.Id);
        }

        [Fact]
        public void Interpret_CommandWithSubjectWithWhitespace_ReturnsValidInterpretation()
        {
            var sentence = "getcontent {mystery doc}";
            var interpretation = _commandInterpreter.Interpret(sentence);
            var expectedCommand = "getcontent";
            var expectedArgument = "mystery doc";
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Equal(expectedArgument, interpretation.Subject.Id);
        }

        [Fact]
        public void Interpret_CommandWithoutSubject_ReturnsValidInterpretation()
        {
            var sentence = "enumerate";
            var interpretation = _commandInterpreter.Interpret(sentence);
            var expectedCommand = "enumerate";
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Null(interpretation.Subject);
        }

        [Fact]
        public void Interpret_CommandWithSubjectAndModifiersWithoutArguments_ReturnsValidInterpretation()
        {
            var sentece = "getcontent {mystery} -plain -colored";
            var interpretation = _commandInterpreter.Interpret(sentece);
            var expectedCommand = "getcontent";
            var expectedSubject = "mystery";
            var expectedModifiers = new[] {"plain", "colored"};
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Equal(expectedSubject, interpretation.Subject.Id);
            Assert.NotNull(interpretation.Modifiers);
            Assert.Equal(expectedModifiers.Length, interpretation.Modifiers.Length);
            Assert.All(expectedModifiers, it => Assert.Contains(it, interpretation.Modifiers.Select(predicate => predicate.Name)));
        }

        [Fact]
        public void Interpret_CommandWithSubjectAndModifierWithArgument_ReturnsValidInterpretation()
        {
            var sentece = "getcontent {mystery} -decrypt[pass]";
            var interpretation = _commandInterpreter.Interpret(sentece);
            var expectedCommand = "getcontent";
            var expectedSubject = "mystery";
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Equal(expectedSubject, interpretation.Subject.Id);
            Assert.NotNull(interpretation.Modifiers);
            Assert.Equal(1, interpretation.Modifiers.Length);
            Assert.Equal("decrypt", interpretation.Modifiers[0].Name);
            Assert.Equal("pass", interpretation.Modifiers[0].Arguments);
        }

        [Fact]
        public void Interpret_CommandWithModifiers_ReturnsValidInterpretation()
        {
            var sentece = "getcontent -plain -colored";
            var interpretation = _commandInterpreter.Interpret(sentece);
            var expectedCommand = "getcontent";
            var expectedParameters = new[] { "plain", "colored" };
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Null(interpretation.Subject);
            Assert.NotNull(interpretation.Modifiers);
            Assert.Equal(expectedParameters.Length, interpretation.Modifiers.Length);
            Assert.All(expectedParameters, it => Assert.Contains(it, interpretation.Modifiers.Select(predicate => predicate.Name)));
        }

        [Fact]
        public void Interpret_CommandWithSeveralSubjectsAndModifiers_ThrowsException()
        {
            var sentece = "getcontent {mystery} {document} -plain -colored";
            var exception = Assert.Throws(typeof(CommandInterpreterException), () => _commandInterpreter.Interpret(sentece));
            Assert.Equal("Invalid number of subjects. Only one subject can be specified.", exception.Message);
        }

        [Fact]
        public void Interpret_CommandWithNewDocumentSubject_ReturnsValidInterpretation()
        {
            var sentece = "add {\"name\": \"mydocument\", \"content\": \"document content\"}";
            var interpretation = _commandInterpreter.Interpret(sentece);
            Assert.IsType<NewDocument>(interpretation.Subject);
            Assert.Equal("mydocument", ((NewDocument)interpretation.Subject).Id);
            Assert.Equal("document content", ((NewDocument)interpretation.Subject).Content);
        }
    }
}
