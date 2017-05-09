﻿using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

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