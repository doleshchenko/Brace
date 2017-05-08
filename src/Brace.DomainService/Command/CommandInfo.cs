using Brace.DomainModel.DocumentProcessing.Subjects;

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
        /// The target of the command. Document or its name (id).
        /// </summary>
        public Subject Subject { get; set; }

        /// <summary>
        /// Parameters to execute a command. For instance key to protect a document.
        /// Can be empty.
        /// </summary>
        public CommandParameter[] Parameters { get; set; }
    }
}