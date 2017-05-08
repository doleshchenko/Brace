using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Subjects;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessor
    {
        /// <summary>
        /// Performs action (with the set of parameters) on the document. 
        /// </summary>
        /// <param name="subject">The document or it's name.</param>
        /// <param name="action">Action which will be performed on the subject.</param>
        /// <param name="actionParameters">The parameters of action.</param>
        /// <returns>Result of the processing.</returns>
        Task<DocumentView> ProcessAsync(Subject subject, ActionType action, ActionParameter[] actionParameters);
    }
}