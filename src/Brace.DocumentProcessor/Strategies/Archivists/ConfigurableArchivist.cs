using Brace.DomainModel.DocumentProcessing;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    public abstract class ConfigurableArchivist : Archivist, IConfigurableArchivist
    {
        protected string _configuration;

        public virtual void Configure(string configuration)
        {
            _configuration = configuration;
        }
    }
}