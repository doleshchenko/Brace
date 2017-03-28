namespace Brace.Models
{
    public class CommandExecutionResult
    {
        public object Content { get; set; }
        public CommandExecutionResultType Type { get; set; }
    }

    public enum CommandExecutionResultType
    {
        Ok,
        Warning,
        Error
    }
}