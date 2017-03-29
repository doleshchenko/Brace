namespace Brace.DomainModel.DocumentProcessing.Decorator.Content
{
    public class DocumentPlainContent : DocumentContent
    {
        public string PlainText { get; set; }
        public override string ContentAsString()
        {
            return PlainText;
        }
    }
}