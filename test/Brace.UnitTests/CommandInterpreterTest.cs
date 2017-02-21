using Brace.Interpretation;
using Xunit;

namespace Brace.UnitTests
{
    public class CommandInterpreterTest
    {
        [Fact]
        public void Iterpret_EmptyCommand_ReturnsEmptyInterpretation()
        {
            var sentence = string.Empty;
            var commandIterpreter = new CommandInterpreter();
            var interpretation = commandIterpreter.Interpret(sentence);
            Assert.Equal(CommandInterpretation.EmptyInterpretation, interpretation);
        }

        [Fact]
        public void Interpret_CommandWithOneArgument_ReturnsValidInterpretation()
        {
            var sentence = "type mystery";
            var commandIterpreter = new CommandInterpreter();
            var interpretation = commandIterpreter.Interpret(sentence);
            var expectedCommand = "type";
            var expectedArgument = "mystery";
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Equal(expectedArgument, interpretation.Argument);
        }

        [Fact]
        public void Interpret_CommandWithoutArgument_ReturnsValidInterpretation()
        {
            var sentence = "type";
            var commandIterpreter = new CommandInterpreter();
            var interpretation = commandIterpreter.Interpret(sentence);
            var expectedCommand = "type";
            string expectedArgument = null;
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Equal(expectedArgument, interpretation.Argument);
        }
    }
}
