using Brace.DomainModel.DocumentProcessing.Decorator.Content;

namespace Brace.DomainModel.DocumentProcessing.Decorator
{
    public abstract class DocumentView
    {
        public DocumentContent Content { get; set; }
        public DocumentViewType Type { get; set; }

        public override string ToString()
        {
            return Content?.ContentAsString();
        }
    }

    public class DocumentView<TContent> : DocumentView where TContent: DocumentContent
    {
        public new TContent Content
        {
            get { return (TContent)base.Content; }
            set { base.Content = value; }
        }
    }
}