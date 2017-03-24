namespace Brace.Models
{
    public class CommandExecutionResult
    {
        public string Content { get; set; }
        public CommandExecutionResultType Type { get; set; }
    }

    public enum CommandExecutionResultType
    {
        Ok,
        Warning,
        Error
    }
}