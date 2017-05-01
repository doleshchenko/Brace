namespace Brace.DomainService.DocumentProcessor
{
    public interface IConfigurableArchivist : IArchivist
    {
        void Configure(string configuration);
    }
}