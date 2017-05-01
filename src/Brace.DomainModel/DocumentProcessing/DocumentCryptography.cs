namespace Brace.DomainModel.DocumentProcessing
{
    public class DocumentCryptography: Entity
    {
        public string DocumentId { get; set; }
        public byte[] Iv { get; set; }
    }
}