namespace Brace.Interpretation
{
    public interface ICommandInterpreter
    {
        CommandInterpretation Interpret(string sentence);
    }
}