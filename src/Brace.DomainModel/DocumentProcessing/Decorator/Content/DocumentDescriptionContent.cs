namespace Brace.DomainModel.DocumentProcessing.Decorator.Content
{
    public class DocumentDescriptionContent : DocumentContent
    {
        public string DocumentName { get; set; }
        public bool IsDocumentVisible { get; set; }
        public bool IsDocumentProtected { get; set; }
        public override string ContentAsString()
        {
            return $"name: {DocumentName}; visible: {IsDocumentVisible}; protected: {IsDocumentProtected}";
        }
    }
}