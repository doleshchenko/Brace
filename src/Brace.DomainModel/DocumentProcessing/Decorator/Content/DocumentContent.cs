namespace Brace.DomainModel.DocumentProcessing.Decorator.Content
{
    public abstract class DocumentContent
    {
        public abstract string ContentAsString();

        public override string ToString()
        {
            return ContentAsString();
        }
    }
}