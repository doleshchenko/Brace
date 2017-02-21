using System;

namespace Brace.Interpretation.Exceptions
{
    public class CommandInterpreterException : Exception
    {
        public CommandInterpreterException(string message) : base(message)
        {
        }
    }
}