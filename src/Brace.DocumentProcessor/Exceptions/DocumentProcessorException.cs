using System;

namespace Brace.DocumentProcessor.Exceptions
{
    public class DocumentProcessorException: Exception
    {
        private readonly string _message;
        public DocumentProcessorException(string message) : base(message)
        {
            _message = message;
        }

        public override string Message => $"Exception occured during processing a document. {_message}";
    }
}