using System;

namespace Brace.DomainService.TypeLinker
{
    public class LinkerException : Exception
    {
        public LinkerException(string message) : base(message)
        {
        }
    }
}