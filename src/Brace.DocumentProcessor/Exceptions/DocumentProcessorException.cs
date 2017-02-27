using System;

namespace Brace.DocumentProcessor.Exceptions
{
    public class DocumentProcessorException: Exception
    {
        public DocumentProcessorException(string message) : base(message)
        {
        }
    }
}