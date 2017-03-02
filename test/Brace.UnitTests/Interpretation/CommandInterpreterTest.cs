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
        public void Interpret_CommandWithArgument_ReturnsValidInterpretation()
        {
            var sentence = "type mystery";
            var interpretation = _commandInterpreter.Interpret(sentence);
            var expectedCommand = "type";
            var expectedArgument = "mystery";
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Equal(expectedArgument, interpretation.Argument);
        }

        [Fact]
        public void Interpret_CommandWithoutArgument_ReturnsValidInterpretation()
        {
            var sentence = "type";
            var interpretation = _commandInterpreter.Interpret(sentence);
            var expectedCommand = "type";
            string expectedArgument = null;
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Equal(expectedArgument, interpretation.Argument);
        }

        [Fact]
        public void Interpret_CommandWithArgumentAndParameters_ReturnsValidInterpretation()
        {
            var sentece = "type mystery -plain -colored";
            var interpretation = _commandInterpreter.Interpret(sentece);
            var expectedCommand = "type";
            var expectedArgument = "mystery";
            var expectedParameters = new[] {"plain", "colored"};
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Equal(expectedArgument, interpretation.Argument);
            Assert.NotNull(interpretation.Parameters);
            Assert.Equal(expectedParameters.Length, interpretation.Parameters.Length);
            Assert.All(expectedParameters, it => Assert.Contains(it, interpretation.Parameters));
        }

        [Fact]
        public void Interpret_CommandWithParameters_ReturnsValidInterpretation()
        {
            var sentece = "type -plain -colored";
            var interpretation = _commandInterpreter.Interpret(sentece);
            var expectedCommand = "type";
            string expectedArgument = null;
            var expectedParameters = new[] { "plain", "colored" };
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Equal(expectedArgument, interpretation.Argument);
            Assert.NotNull(interpretation.Parameters);
            Assert.Equal(expectedParameters.Length, interpretation.Parameters.Length);
            Assert.All(expectedParameters, it => Assert.Contains(it, interpretation.Parameters));
        }

        [Fact]
        public void Interpret_CommandWithSeveralArgumentAndParameters_ThrowsException()
        {
            var sentece = "type mystery document -plain -colored";
            var exception = Assert.Throws(typeof(CommandInterpreterException), () => _commandInterpreter.Interpret(sentece));
            Assert.Equal("Invalid number of arguments. Only one argument can be specified.", exception.Message);
        }
    }
}
