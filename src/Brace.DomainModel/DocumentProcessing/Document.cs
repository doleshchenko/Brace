namespace Brace.DomainModel.DocumentProcessing
{
    public class Document : Entity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsVisible { get; set; }
        public bool IsProtected { get; set; }
    }
}
