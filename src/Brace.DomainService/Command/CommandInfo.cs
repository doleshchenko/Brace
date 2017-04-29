namespace Brace.DomainService.Command
{
    public class CommandInfo
    {
        /// <summary>
        /// The command name.
        /// enumerate, getcontent, update, add, delete, etc.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// In most of cases it's document on which command will be executed.
        /// Can be empty.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Parameters to execute a command. For instance key to protect a document.
        /// Can be empty.
        /// </summary>
        public CommandParameter[] Parameters { get; set; }
    }
}