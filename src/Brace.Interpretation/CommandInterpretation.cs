namespace Brace.Interpretation
{
    public class CommandInterpretation
    {
        private static CommandInterpretation _emptyInterpretation;

        public static CommandInterpretation EmptyInterpretation => _emptyInterpretation ?? (_emptyInterpretation = new CommandInterpretation());

        public string Command { get; set; }
        public string Argument { get; set; }
    }
}