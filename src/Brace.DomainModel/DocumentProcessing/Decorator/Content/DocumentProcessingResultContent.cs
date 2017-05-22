using System.Text.RegularExpressions;

namespace Brace.DomainModel.DocumentProcessing.Decorator.Content
{
    public class DocumentProcessingResultContent : DocumentContent
    {
        public DocumentProcessingResultType ProcessingResultType { get; set; }
        public string Description { get; set; }

        public override string ContentAsString()
        {
            string result;
            if (ProcessingResultType == DocumentProcessingResultType.AddFailed
                || ProcessingResultType == DocumentProcessingResultType.DeleteFailed
                || ProcessingResultType == DocumentProcessingResultType.UpdateFailed
                || ProcessingResultType == DocumentProcessingResultType.GetFailed)
            {
                result = $"{Regex.Replace(ProcessingResultType.ToString(), "([A-Z])", " $1").Trim()}.";
            }
            else
            {
                result = $"Document {ProcessingResultType}.";
            }
            if (!string.IsNullOrEmpty(Description))
            {
                result += $" {Description}";
            }
            return result;
        }
    }

    public enum DocumentProcessingResultType
    {
        Added,
        Updated,
        Deleted,

        AddFailed,
        UpdateFailed,
        DeleteFailed,
        GetFailed
    }
}