using System;

namespace Brace.DocumentProcessor.Exceptions
{
    public class DocumentProcessingStrategyException : Exception
    {
        public DocumentProcessingStrategyException(string message) : base(message)
        {
        }
    }
}