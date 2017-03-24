namespace Brace.Commands.Validation
{
    public class CommandValidationResult
    {
        private static CommandValidationResult _validValidationResult;
        public static CommandValidationResult Valid
        {
            get
            {
                if (_validValidationResult == null)
                {
                    _validValidationResult = new CommandValidationResult();
                }
                return _validValidationResult;
            }
        }

        public CommandValidationResult()
        {
            IsValid = true;
        }

        public bool IsValid { get; set; }
        public string ValidationMessage { get; set; }
    }
}