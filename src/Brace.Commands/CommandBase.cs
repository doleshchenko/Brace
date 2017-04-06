using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected readonly IDocumentProcessor _documentProcessor;
        private string _argument;
        private string[] _parameters;
        private string _commandText;

        protected CommandBase(IDocumentProcessor documentProcessor)
        {
            _documentProcessor = documentProcessor;
            CreationDate = DateTime.Now;
        }
        public DateTime CreationDate { get; }
        public string CommandText => _commandText;
        public string Argument => _argument;
        public string[] Parameters => _parameters;
       
        public virtual async Task<DocumentView> ExecuteAsync()
        {
            return await _documentProcessor.ProcessAsync(_argument, GetActionType(), _parameters);
        }

        public void SetParameters(string commandText, string argument, string[] parameters)
        {
            _argument = argument;
            _parameters = parameters;
            _commandText = commandText;
        }

        public virtual CommandValidationResult Validate()
        {
            var result = new CommandValidationResult();
            if (Parameters != null && Parameters.Any())
            {
                var allParametersDistinct = Parameters.Distinct();
                if (allParametersDistinct.Count() != Parameters.Length)
                {
                    result.IsValid = false;
                    result.ValidationMessage = "Invalid command parameters: duplicates found";
                }
                var archivists = GetAssociatedArchivists();
                foreach (var parameter in Parameters)
                {
                    if (!archivists.Contains(parameter))
                    {
                        result.IsValid = false;
                        result.ValidationMessage = $"Invalid command parameters: parameter '{parameter}' can't be used with the '{GetActionType().ToString().ToLowerInvariant()}' command";
                        break;
                    }
                }
            }
            return result;
        }

        protected string[] GetAssociatedArchivists()
        {
            var attribute = (CommandAttribute)GetType().GetTypeInfo().GetCustomAttributes(typeof(CommandAttribute)).Single();
            var archivists = attribute.AssociatedArchivists;
            var type = typeof(ArchivistType);
            var result = new List<string>();
            foreach (var archivistType in archivists)
            {
                var memberInfo = type.GetMember(archivistType.ToString());
                var archivistTypeAttribute = memberInfo[0].GetCustomAttribute(typeof(ArchivistTypeDescriptionAttribute)) as ArchivistTypeDescriptionAttribute;
                if (archivistTypeAttribute != null)
                {
                    result.Add(archivistTypeAttribute.ArchivistActionName);
                }
            }
            return result.ToArray();
        }

        protected abstract ActionType GetActionType();

    }
}