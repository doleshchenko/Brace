using System.Collections;

namespace Brace.DomainModel.DocumentProcessing
{
    public abstract class DocumentView
    {
        public object Content { get; set; }
        public DocumentViewType Type { get; set; }

        public override string ToString()
        {
            return Content?.ToString();
        }
    }

    public class DocumentView<TContent> : DocumentView
    {
        public new TContent Content
        {
            get { return (TContent)base.Content; }
            set { base.Content = value; }
        }

        public override string ToString()
        {
            if (Content is IEnumerable)
            {
                return string.Join("\n", (IEnumerable) Content);
            }
            return Content?.ToString();
        }
    }
}