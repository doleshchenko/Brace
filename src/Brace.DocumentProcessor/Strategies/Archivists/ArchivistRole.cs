namespace Brace.DocumentProcessor.Strategies.Archivists
{
    public enum ArchivistRole
    {
        DoNothing,
        Encrypt,
        Decrypt,
        MakeVisible,
        MakeInvisible,
        GetVisible,
        GetInvisible,
        GetAll,
        GetStatus
    }
}