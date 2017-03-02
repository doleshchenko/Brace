using System;

namespace Brace.DomainService.Exceptions
{
    public class DomainServiceException : Exception
    {
        public DomainServiceException(string message) : base(message)
        {
        }
    }
}