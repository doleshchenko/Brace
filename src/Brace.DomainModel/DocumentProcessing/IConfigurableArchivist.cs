namespace Brace.DomainModel.DocumentProcessing
{
    public interface IConfigurableArchivist : IArchivist
    {
        void Configure(string configuration);
    }
}