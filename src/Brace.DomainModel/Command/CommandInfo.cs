using Brace.DomainModel.Command.Subjects;

namespace Brace.DomainModel.Command
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
        /// Predicates to execute a command. For instance, a key to protect a document - encrypt[password]. Where 'encrypt' is a parameter name and  'password' is an argument of the 'encrypt'.
        /// Can be empty.
        /// </summary>
        public Predicate[] Predicates { get; set; }
    }
}