using System;

namespace Brace.Commands.Factory
{
    public class CommandLinkerException : Exception
    {
        public CommandLinkerException(string message) : base(message)
        {
        }
    }
}