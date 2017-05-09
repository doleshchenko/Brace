using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Brace.Commands.Validation;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected readonly IDocumentProcessor _documentProcessor;
        private Subject _subject;
        private Modifier[] _modifiers;
        private string _commandId;

        protected CommandBase(IDocumentProcessor documentProcessor)
        {
            _documentProcessor = documentProcessor;
            CreationDate = DateTime.Now;
        }
        public DateTime CreationDate { get; }
        public string CommandText => _commandId;
        public Subject Subject => _subject;
        public Modifier[] Modifiers => _modifiers;
       
        public virtual async Task<DocumentView> ExecuteAsync()
        {
            return await _documentProcessor.ProcessAsync(_subject, GetActionType(),
                _modifiers?.Select(it => new ActionParameter
                    {
                        Name = it.Name,
                        Data = it.Arguments
                    })
                    .ToArray());
        }

        public void SetParameters(string commandId, Subject subject, Modifier[] modifiers)
        {
            _subject = subject;
            _modifiers = modifiers;
            _commandId = commandId;
        }

        public virtual CommandValidationResult Validate()
        {
            var result = new CommandValidationResult();
            if (Modifiers != null && Modifiers.Any())
            {
                var allParametersDistinct = Modifiers.GroupBy(it => it.Name);
                if (allParametersDistinct.Count() != Modifiers.Length)
                {
                    result.IsValid = false;
                    result.ValidationMessage = "Invalid command modifiers: duplicates found";
                }
                var archivists = GetAssociatedArchivists();
                foreach (var parameter in Modifiers)
                {
                    if (!archivists.Contains(parameter.Name))
                    {
                        result.IsValid = false;
                        result.ValidationMessage = $"Invalid command modifiers: parameter '{parameter.Name}' can't be used with the '{GetActionType().ToString().ToLowerInvariant()}' command";
                        break;
                    }
                }
            }
            return result;
        }

        private string[] GetAssociatedArchivists()
        {
            var attribute = (CommandAttribute)GetType().GetTypeInfo().GetCustomAttributes(typeof(CommandAttribute)).Single();
            var archivists = attribute.AssociatedArchivists;
            var type = typeof(ArchivistType);
            var result = new List<string>();
            foreach (var archivistType in archivists)
            {
                var memberInfo = type.GetMember(archivistType.ToString());
                if (memberInfo[0].GetCustomAttribute(typeof(ArchivistTypeDescriptionAttribute)) is
                    ArchivistTypeDescriptionAttribute
                    archivistTypeAttribute)
                {
                    result.Add(archivistTypeAttribute.ArchivistActionName);
                }
            }
            return result.ToArray();
        }

        protected abstract ActionType GetActionType();
    }
}