﻿namespace Brace.DomainModel.DocumentProcessing
{
    public class Document : Entity
    {
        public string Name { get; set; }
        public bool IsProtected { get; set; }
        public string Content { get; set; }
    }
}
