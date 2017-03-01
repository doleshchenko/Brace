using System;
using System.Linq;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists.Factory
{
    public class ArchivistFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ArchivistFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IArchivist CreateArchivistChain(string[] actionsToPerform)
        {
            if (actionsToPerform == null || !actionsToPerform.Any())
            {
                throw new NotImplementedException();
                //return _serviceProvider.Resolve<IArchivist>(typeof(DoNothingArhivist).Name);
            }
            throw new NotImplementedException();
        }
    }
}