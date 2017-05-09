using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.Interpretation.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Brace.Interpretation
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private const string SubjectPattern = @"\{(.*?)\}";
        private const string CommandIdPatter = @"(^\w)\w+";
        private const string ModifiersParametersPattern = @"((-\w*)(?:\[(.*?)\])?)";

        public CommandInterpretation Interpret(string sentence)
        {

            if (string.IsNullOrWhiteSpace(sentence))
            {
                return CommandInterpretation.EmptyInterpretation;
            }

            var commandIdMatch = GetCommandId(sentence);
            var subject = GetSubject(sentence);
            var modifiers = GetModifiers(sentence);

            return new CommandInterpretation
            {
                Command = commandIdMatch,
                Subject = subject,
                Modifiers = modifiers.ToArray()
            };
        }

        private static List<Modifier> GetModifiers(string sentence)
        {
            var modifierMatch = Regex.Matches(sentence, ModifiersParametersPattern);
            var modifiers = new List<Modifier>();
            if (modifierMatch.Count != 0)
            {
                foreach (Match mm in modifierMatch)
                {
                    var modifier = new Modifier();
                    var argumentStartIndex = mm.Value.IndexOf('[');
                    if (argumentStartIndex != -1)
                    {
                        modifier.Name = mm.Value.Substring(1, argumentStartIndex - 1);
                        modifier.Arguments = mm.Value.Substring(argumentStartIndex + 1,
                            mm.Value.Length - (argumentStartIndex + 1) - 1);
                    }
                    else
                    {
                        modifier.Name = mm.Value.Remove(0, 1);
                    }
                    modifiers.Add(modifier);
                }
            }
            return modifiers;
        }

        private static string GetCommandId(string sentence)
        {
            var commandIdMatch = Regex.Matches(sentence, CommandIdPatter);
            if (commandIdMatch.Count != 1)
            {
                throw new CommandInterpreterException("Cannot indentify the command id.");
            }
            return commandIdMatch[0].Value;
        }

        private static Subject GetSubject(string sentence)
        {
            Subject subject = null;
            var subjectMatch = Regex.Matches(sentence, SubjectPattern);
            if (subjectMatch.Count != 0)
            {
                if (subjectMatch.Count > 1)
                {
                    throw new CommandInterpreterException("Invalid number of subjects. Only one subject can be specified.");
                }
                try
                {
                    string documentName = null;
                    string documentContent = null;
                    var hasContent = false;
                    var dynamicSubject = JsonConvert.DeserializeObject(subjectMatch[0].Value);
                    foreach (JProperty token in (IEnumerable) dynamicSubject)
                    {
                        if (token.Name == "name")
                        {
                            documentName = token.Value.ToString();
                        }
                        if (token.Name == "content")
                        {
                            hasContent = true;
                            documentContent = token.Value?.ToString();
                        }
                    }
                    subject = hasContent
                        ? new NewDocument {Id = documentName, Content = documentContent}
                        : new DocumentName {Id = documentName};
                }
                catch
                {
                    subject = new DocumentName {Id = subjectMatch[0].Value.Trim('{', '}')};
                }
            }
            return subject;
        }
    }
}
