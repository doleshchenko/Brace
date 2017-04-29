using System;
using System.Linq;
using Brace.Interpretation.Exceptions;

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
            var command = commandChanks[0];
            var arguments = commandChanks.Skip(1).Where(it => !it.StartsWith("-")).ToArray();
            string argument = null;
            if (arguments.Length > 1)
            {
                throw new CommandInterpreterException("Invalid number of arguments. Only one argument can be specified.");
            }
            if (arguments.Length == 1)
            {
                argument = arguments[0];
            }
            var parameters = commandChanks.Where(it => it.StartsWith("-")).Select(it => it.Remove(0,1)).ToArray();
            return new CommandInterpretation {Command = command, Argument = argument, Parameters = parameters.ToDictionary(it => it, it => string.Empty)};
        }
    }
}
