using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brace.Interpretation
{
    public class CommandInterpreter : ICommandInterpreter
    {
        public CommandInterpretation Interpret(string sentence)
        {
            if (string.IsNullOrWhiteSpace(sentence))
            {
                return CommandInterpretation.EmptyInterpretation;
            }
            var commandChanks = sentence.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            return new CommandInterpretation {Command = commandChanks[0], Argument = commandChanks[1]};
        }
    }
}
