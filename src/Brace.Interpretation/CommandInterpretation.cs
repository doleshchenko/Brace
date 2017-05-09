using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;

namespace Brace.Interpretation
{
    public class CommandInterpretation
    {
        private static CommandInterpretation _emptyInterpretation;

        public static CommandInterpretation EmptyInterpretation => _emptyInterpretation ?? (_emptyInterpretation = new CommandInterpretation{Command = "void"});

        public string Command { get; set; }
        public Subject Subject { get; set; }
        public Modifier[] Modifiers { get; set; }
    }
}