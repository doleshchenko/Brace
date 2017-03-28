namespace Brace.DomainModel.DocumentProcessing
{
    public class DocumentWithoutContent : Entity
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public bool IsProtected { get; set; }
    }
}