namespace Brace.DomainModel.DocumentProcessing
{
    public interface IArchivist
    {
        Document Rethink(Document document);
        IArchivist Successor { get; set; }
    }
}