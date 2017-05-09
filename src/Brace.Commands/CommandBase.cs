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
        private string _commandText;
        private Predicate[] _predicates;

        protected CommandBase(IDocumentProcessor documentProcessor)
        {
            _documentProcessor = documentProcessor;
            CreationDate = DateTime.Now;
        }
        public DateTime CreationDate { get; }
        public string CommandText => _commandText;
        public Subject Subject => _subject;
        public Predicate[] Predicates => _predicates;
       
        public virtual async Task<DocumentView> ExecuteAsync()
        {
            return await _documentProcessor.ProcessAsync(_subject, GetActionType(),
                _predicates?.Select(it => new ActionParameter
                    {
                        Name = it.Name,
                        Data = it.Arguments
                    })
                    .ToArray());
        }

        public void SetParameters(string commandText, Subject subject, Predicate[] predicates)
        {
            _subject = subject;
            _predicates = predicates;
            _commandText = commandText;
        }

        public virtual CommandValidationResult Validate()
        {
            var result = new CommandValidationResult();
            if (Predicates != null && Predicates.Any())
            {
                var allParametersDistinct = Predicates.GroupBy(it => it.Name);
                if (allParametersDistinct.Count() != Predicates.Length)
                {
                    result.IsValid = false;
                    result.ValidationMessage = "Invalid command predicates: duplicates found";
                }
                var archivists = GetAssociatedArchivists();
                foreach (var parameter in Predicates)
                {
                    if (!archivists.Contains(parameter.Name))
                    {
                        result.IsValid = false;
                        result.ValidationMessage = $"Invalid command predicates: parameter '{parameter.Name}' can't be used with the '{GetActionType().ToString().ToLowerInvariant()}' command";
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