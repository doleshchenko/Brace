namespace Brace.DomainModel.Command.Subjects
{
    public class UpdateDocumentSubject : AddDocumentSubject
    {
        public bool ContentUpdateRequired { get; set; }
    }
}