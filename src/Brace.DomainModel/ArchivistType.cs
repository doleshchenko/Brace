namespace Brace.DomainModel
{
    public enum ArchivistType
    {
        [ArchivistTypeDescription("")]
        DoNothing,
        [ArchivistTypeDescription("encrypt")]
        Encrypt,
        [ArchivistTypeDescription("decrypt")]
        Decrypt,
        [ArchivistTypeDescription("makevisible")]
        MakeVisible,
        [ArchivistTypeDescription("makeinvisible")]
        MakeInvisible,
        [ArchivistTypeDescription("getvisible")]
        GetVisible,
        [ArchivistTypeDescription("getinvisible")]
        GetInvisible,
        [ArchivistTypeDescription("getall")]
        GetAll,
        [ArchivistTypeDescription("getstatus")]
        GetStatus
    }
}