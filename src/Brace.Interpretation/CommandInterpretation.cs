namespace Brace.Interpretation
{
    public class CommandInterpretation
    {
        private static CommandInterpretation _emptyInterpretation;

        public static CommandInterpretation EmptyInterpretation => _emptyInterpretation ?? (_emptyInterpretation = new CommandInterpretation{Command = "void"});

        public string Command { get; set; }
        public string Argument { get; set; }
        public string[] Parameters { get; set; }
        //UI -> command interpetation -> command -> bus -> handler -> bus -> ui
    }
}