namespace Brace.DomainModel.DocumentProcessing
{
    public abstract class DocumentView
    {
        public object Content { get; set; }
        public DocumentViewType Type { get; set; }
    }

    public class DocumentView<TContent> : DocumentView
    {
        public new TContent Content
        {
            get { return (TContent)base.Content; }
            set { base.Content = value; }
        }
    }
}