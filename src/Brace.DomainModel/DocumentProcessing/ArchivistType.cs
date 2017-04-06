using Brace.DomainModel.DocumentProcessing.Attributes;

namespace Brace.DomainModel.DocumentProcessing
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
        [ArchivistTypeDescription("visible")]
        GetVisible,
        [ArchivistTypeDescription("invisible")]
        GetInvisible,
        [ArchivistTypeDescription("getstatus")]
        GetStatus
    }
}