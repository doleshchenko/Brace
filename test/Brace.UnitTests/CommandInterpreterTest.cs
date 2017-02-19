using Brace.Interpretation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Brace.UnitTests
{
    public class CommandInterpreterTest
    {   
        [Fact]
        public void Interpret_CommandTypeAndArgumentMystery_ReturnsValidInterpretation()
        {
            var commandText = "type mystery";
            var commandIterpreter = new CommandInterpreter();
            var interpretation = commandIterpreter.Interpret(commandText);
            var expectedCommand = "type";
            var expectedArgument = "mystery";
            Assert.Equal(expectedCommand, interpretation.Command);
            Assert.Equal(expectedArgument, interpretation.Argument);
        }
    }
}
